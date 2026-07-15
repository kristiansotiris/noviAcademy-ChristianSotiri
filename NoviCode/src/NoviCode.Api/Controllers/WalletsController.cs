using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoviCode.Commands.Wallets;
using NoviCode.Queries.Wallets;
namespace NoviCode.Api;

[ApiController]
[Route("wallets")]
public class WalletsController : ControllerBase
{
	private readonly IWalletService _wallets;
	private readonly IMediator _mediator;


	public WalletsController(IWalletService wallets, IMediator mediator)
	{
		_wallets = wallets;
		_mediator = mediator;
    }

	// POST /wallets — create a wallet, return 201 Created with a Location header.
	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateWalletRequest request, CancellationToken cancellationToken)
	{
		var wallet = _mediator.Send(request);
		return CreatedAtAction(nameof(GetById), wallet);
	}

	// GET /wallets/{id} — caching handled transparently by the caching decorator on IWalletService.
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
	{
		var wallet = await _mediator.Send(new GetByIdQuery(id));
		return wallet is null ? NotFound() : Ok(WalletResponse.From(wallet));
	}

	// POST /wallets/{id}/deposit — deposit funds (the decorator refreshes the cache on success).
	[HttpPost("{id:guid}/deposit")]
	public async Task<IActionResult> Deposit(Guid id, [FromBody] DepositRequest request, CancellationToken cancellationToken)
	{
		try
		{
			var wallet = await _mediator.Send(new DepositCommand(id, request.Amount), cancellationToken);
			return wallet is null ? NotFound() : Ok(WalletResponse.From(wallet));
		}
		catch (WalletException ex)
		{
			// Business-rule violation (blocked wallet, non-positive amount) → 400.
			return BadRequest(new { error = ex.Message });
		}
	}
}

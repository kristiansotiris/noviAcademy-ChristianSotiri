using Microsoft.Extensions.Logging;
using WorldRank.src.WorldRank.Application.Interfaces;
using WorldRank.src.WorldRank.Application.Strategies;
using WorldRank.src.WorldRank.Domain.Entities;
using WorldRank.src.WorldRank.Domain.Exceptions;
using SysConsole = System.Console;
namespace WorldRank.src.WorldRank.Application.Services;

public class WalletService
{
	private readonly IWalletRepository _walletRepository;
	private readonly IPlayerRepository _playerRepository;
	private readonly ILogger<WalletService> _logger;
	private readonly IReadOnlyDictionary<FundsOperation, IFundsStrategy> _fundsStrategies;

	public WalletService(
		IWalletRepository walletRepository,
		IPlayerRepository playerRepository,
		IEnumerable<IFundsStrategy> strategies,
		ILogger<WalletService> logger)
	{
		_walletRepository = walletRepository;
		_playerRepository = playerRepository;
		_logger = logger;

		// Index every registered strategy by the operation it implements.
		_fundsStrategies = strategies.ToDictionary(strategy => strategy.Operation);
	}

	public void AddWalletToPlayer()
	{
		var playerId = Prompts.PromptPlayerId();
		if (playerId is null)
			return;

		var currency = Prompts.PromptCurrency();
		if (currency is null)
			return;

		var balance = Prompts.PromptAmount("Initial balance");
		if (balance is null)
			return;

		try
		{
			if (_playerRepository.FindPlayer(playerId.Value) is null)
				throw new PlayerNotFoundException(playerId.Value);

			var wallet = new Wallet(GenerateWalletId(), playerId.Value, currency.Value, balance.Value);
			_walletRepository.Add(wallet);
			SysConsole.WriteLine("Wallet added successfully.");
		}
		catch (PlayerNotFoundException ex)
		{
			_logger.LogWarning(ex, "Could not add wallet, player {PlayerId} not found", playerId);
            SysConsole.WriteLine($"Error: {ex.Message}");
		}
		catch (WalletException ex)
		{
			_logger.LogWarning(ex, "Could not add wallet for player {PlayerId} in {Currency}", playerId, currency);
            SysConsole.WriteLine($"Error: {ex.Message}");
		}
	}

	public void GetWalletsOfPlayer()
	{
		var playerId = Prompts.PromptPlayerId();
		if (playerId is null)
			return;

		var wallets = _walletRepository.GetAllWalletsByPlayerId(playerId.Value);

		if (wallets.Count == 0)
		{
            SysConsole.WriteLine("No wallets found for this player.");
			return;
		}

		foreach (var wallet in wallets)
            SysConsole.WriteLine($"Wallet Number {wallets.IndexOf(wallet)} {wallet}");
	}

	public void DepositToWallet()
	{
		var playerId = Prompts.PromptPlayerId();
		if (playerId is null)
			return;

		var currency = Prompts.PromptCurrency();
		if (currency is null)
			return;

		var amount = Prompts.PromptAmount("Amount to deposit");
		if (amount is null)
			return;

		RunWalletOperation(() =>
		{
			_walletRepository.Deposit(playerId.Value, currency.Value, amount.Value);
            SysConsole.WriteLine("Deposit successful.");
		});
	}

	public void WithdrawFromWallet()
	{
		var playerId = Prompts.PromptPlayerId();
		if (playerId is null)
			return;

		var currency = Prompts.PromptCurrency();
		if (currency is null)
			return;

		var amount = Prompts.PromptAmount("Amount to withdraw");
		if (amount is null)
			return;

		RunWalletOperation(() =>
		{
			_walletRepository.Withdraw(playerId.Value, currency.Value, amount.Value);
            SysConsole.WriteLine("Withdrawal successful.");
		});
	}

	public void BlockWallet()
	{
		var playerId = Prompts.PromptPlayerId();
		if (playerId is null)
			return;

		var currency = Prompts.PromptCurrency();
		if (currency is null)
			return;

		RunWalletOperation(() =>
		{
			_walletRepository.Block(playerId.Value, currency.Value);
            SysConsole.WriteLine("Wallet blocked.");
		});
	}

	public void UnblockWallet()
	{
		var playerId = Prompts.PromptPlayerId();
		if (playerId is null)
			return;

		var currency = Prompts.PromptCurrency();
		if (currency is null)
			return;

		RunWalletOperation(() =>
		{
			_walletRepository.Unblock(playerId.Value, currency.Value);
            SysConsole.WriteLine("Wallet unblocked.");
		});
	}

	public void UpdateWalletBalance()
	{
		var playerId = Prompts.PromptPlayerId();
		if (playerId is null)
			return;

		var currency = Prompts.PromptCurrency();
		if (currency is null)
			return;

		var newBalance = Prompts.PromptAmount("New balance");
		if (newBalance is null)
			return;

		RunWalletOperation(() =>
		{
			_walletRepository.UpdateBalance(playerId.Value, currency.Value, newBalance.Value);
            SysConsole.WriteLine("Balance updated.");
		});
	}

	public void ApplyFundsStrategy()
	{
		var playerId = Prompts.PromptPlayerId();
		if (playerId is null)
			return;

		var currency = Prompts.PromptCurrency();
		if (currency is null)
			return;

		var operation = Prompts.PromptFundsOperation();
		if (operation is null)
			return;

		var amount = Prompts.PromptAmount("Amount");
		if (amount is null)
			return;

		// Pick the strategy that matches the chosen operation (resolved from DI, no factory).
		var strategy = _fundsStrategies[operation.Value];

		RunWalletOperation(() =>
		{
			var wallet = _walletRepository.GetWallet(playerId.Value, currency.Value);
			strategy.Execute(wallet, amount.Value);
            _logger.LogInformation("Applied {Strategy} of {Amount} to player {PlayerId} {Currency} wallet (balance {Balance})",
				strategy.GetType().Name, amount, playerId, currency, wallet.Balance);
            SysConsole.WriteLine($"{operation} operation applied.");
		});
	}

	// Runs a wallet operation and turns any domain (WalletException) failure into a friendly message + log.
	private void RunWalletOperation(Action operation)
	{
		try
		{
			operation();
		}
		catch (WalletException ex)
		{
			_logger.LogWarning(ex, "Wallet operation failed");
            SysConsole.WriteLine($"Error: {ex.Message}");
		}
	}

	private int GenerateWalletId()
	{
		var existingIds = _walletRepository.GetAll().Select(p => p.Id).ToHashSet();

		int id;
		do
		{
			id = Random.Shared.Next(1, int.MaxValue);
		}
		while (existingIds.Contains(id));

		return id;
	}
}

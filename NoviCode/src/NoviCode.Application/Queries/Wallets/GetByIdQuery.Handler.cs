using MediatR;
namespace NoviCode.Queries.Wallets
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Wallet>
    {
        private readonly IWalletRepository _wallets;

        public GetByIdQueryHandler(IWalletRepository wallets) => _wallets = wallets;

        public async Task<Wallet> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _wallets.GetByIdAsync(request.WalletId, cancellationToken);

            if (wallet is null) return null!;

            return wallet!;

        }
    }
}

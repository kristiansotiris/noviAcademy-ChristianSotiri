using MediatR;

namespace NoviCode.Queries.Wallets
{
    public class GetAllWalletsQueryHandler : IRequestHandler<GetAllWalletsQuery, IReadOnlyList<Wallet>>
    {
        private readonly IWalletRepository _wallets;

        public GetAllWalletsQueryHandler(IWalletRepository wallets) => _wallets = wallets;
        public async Task<IReadOnlyList<Wallet>> Handle(GetAllWalletsQuery request, CancellationToken cancellationToken)
        {
            var wallets = await _wallets.GetAllAsync(cancellationToken);

            if (wallets.Count == 0) return null!;

            return wallets;
        }
    }
}

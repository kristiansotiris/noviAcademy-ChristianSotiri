using MediatR;

namespace NoviCode.Commands.Wallets
{
    public class DepositeCommandHandler : IRequestHandler<DepositCommand, Wallet>
    {
        private readonly IWalletRepository _wallets;

        public DepositeCommandHandler(IWalletRepository wallets) => _wallets = wallets;

        public async Task<Wallet> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _wallets.GetByIdAsync(request.WalletId, cancellationToken);

            if (wallet is null) return null!;

            wallet.Deposit(request.Amount);
            await _wallets.SaveChangesAsync(cancellationToken);
            return wallet;

        }
    }
}

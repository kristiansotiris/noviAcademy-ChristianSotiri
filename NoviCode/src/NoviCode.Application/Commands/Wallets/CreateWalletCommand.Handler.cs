using MediatR;

namespace NoviCode.Commands.Wallets
{
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, Wallet>
    {
        private readonly IWalletRepository _wallets;
        public CreateWalletCommandHandler(IWalletRepository wallets) => _wallets = wallets;

        public async Task<Wallet> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = new Wallet(request.PlayerId, request.Currency);
            await _wallets.AddAsync(wallet, cancellationToken);
            return wallet;
        }
    }
}
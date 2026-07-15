using MediatR;

namespace NoviCode.Commands.Wallets
{
    public class SetBlockedCommandHandler : IRequestHandler<SetBlockedCommand, Wallet?>
    {
        private readonly IWalletRepository _wallets;    

        public SetBlockedCommandHandler(IWalletRepository wallets) => _wallets = wallets;

        public async Task<Wallet?> Handle(SetBlockedCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _wallets.GetByIdAsync(request.Id);
            if (wallet is not null) wallet.Block();
            return wallet;
        }
    }
}

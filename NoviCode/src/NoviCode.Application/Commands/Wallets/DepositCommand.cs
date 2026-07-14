
using MediatR;

namespace NoviCode.Commands.Wallets
{
    public record DepositCommand(Guid WalletId, decimal Amount) : IRequest<Wallet>;
}

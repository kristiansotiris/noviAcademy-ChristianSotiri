using MediatR;

namespace NoviCode.Commands.Wallets
{
    public record SetBlockedCommand(Guid Id, bool Blocked, CancellationToken CancellationToken) : IRequest<Wallet?>;
}

using MediatR;

namespace NoviCode.Queries.Wallets
{
    public record GetByIdQuery(Guid WalletId) : IRequest<Wallet?>;
}

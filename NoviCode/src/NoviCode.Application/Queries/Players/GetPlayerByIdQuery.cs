using MediatR;

namespace NoviCode.Queries.Players
{
    public record GetPlayerByIdQuery(Guid playerId) : IRequest<Player>;
}

using MediatR;

namespace NoviCode.Queries.Players
{
    public record GetAllPLayersQuery() : IRequest<IReadOnlyList<Player>>{ }
}

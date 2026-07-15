using MediatR;
using NoviCode.Exceptions;
namespace NoviCode.Queries.Players
{
    public class GetAllPlayersQueryHandler : IRequestHandler<GetAllPLayersQuery, IReadOnlyList<Player>>
    {
        private readonly IPlayerRepository _players;

        public GetAllPlayersQueryHandler(IPlayerRepository playerRepository) => _players = playerRepository;

        public async Task<IReadOnlyList<Player>> Handle(GetAllPLayersQuery request, CancellationToken cancellationToken)
        {
            var players = await _players.GetAllAsync(cancellationToken);
            return players ?? throw new PlayerNotFoundException();
        }
    }
}

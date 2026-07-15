using MediatR;
using NoviCode.Exceptions;

namespace NoviCode.Queries.Players
{
    public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, Player?>
    {
        private readonly IPlayerRepository _players;

        public GetPlayerByIdQueryHandler(IPlayerRepository players)
        {
            _players = players;
        }
        public async Task<Player?> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
        {
            var player = await  _players.GetByIdAsync(request.playerId);
            return player ?? throw new PlayerNotFoundException();
        }
    }
}

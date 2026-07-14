using MediatR;

namespace NoviCode.Commands.Players
{
    public record CreatePlayerCommand(string name, int score) : IRequest<Guid>;
}

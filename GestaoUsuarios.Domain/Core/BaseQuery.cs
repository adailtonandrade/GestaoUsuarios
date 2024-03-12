using MediatR;

namespace GestaoUsuarios.Domain.Core
{
    public record BaseQuery<TResponse> : IRequest<TResponse>
    {
    }
}

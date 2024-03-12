using GestaoUsuarios.Application.Clientes.Response;
using GestaoUsuarios.Domain.Core;

namespace GestaoUsuarios.Application.Clientes.Queries
{
    public record ObterTodosOsClientesQuery : BaseQuery<List<ClienteResponse>>
    {
    }
}

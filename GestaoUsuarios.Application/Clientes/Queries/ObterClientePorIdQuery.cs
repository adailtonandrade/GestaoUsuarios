using GestaoUsuarios.Application.Clientes.Response;
using GestaoUsuarios.Domain.Core;

namespace GestaoUsuarios.Application.Clientes.Queries
{
    public record ObterClientePorIdQuery : BaseQuery<ClienteModelResponse>
    {
        public int Id { get; set; }
        public ObterClientePorIdQuery(int id)
        {
            Id = id;
        }
    }
}

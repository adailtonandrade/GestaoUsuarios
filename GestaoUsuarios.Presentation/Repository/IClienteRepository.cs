using GestaoUsuarios.Application.Clientes.Response;
using GestaoUsuarios.Presentation.Models;

namespace GestaoUsuarios.Presentation.Repository
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteResponse>> ObterClientesAsync();
        Task<bool> CadastrarClienteAsync(ClienteModel cliente);
        Task<bool> AtualizarClienteAsync(ClienteModel cliente);
        Task<ClienteModel?> ObterClientePorIdAsync(int id);
        Task<bool> DesativarClienteByIdAsync(int id);
        //Task<IEnumerable<ClienteResponse>> GetReviewSummariesAsync();
    }
}

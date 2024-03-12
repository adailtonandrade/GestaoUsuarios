using GestaoUsuarios.Application.Clientes.Queries;
using GestaoUsuarios.Application.Clientes.Response;
using GestaoUsuarios.Domain.Interfaces;
using MediatR;

namespace GestaoUsuarios.Application.Clientes.Handlers
{
    public class ObterClientePorIdQueryHandler : IRequestHandler<ObterClientePorIdQuery, ClienteModelResponse?>
    {
        private readonly IClienteRepository _clienteRepository;
        public ObterClientePorIdQueryHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<ClienteModelResponse?> Handle(ObterClientePorIdQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetByIdAsync(request.Id, cancellationToken);
            if (cliente == null)
                return null;

            return new ClienteModelResponse
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                PorteEmpresa = (int?)cliente.PorteEmpresa
            };
        }
    }
}

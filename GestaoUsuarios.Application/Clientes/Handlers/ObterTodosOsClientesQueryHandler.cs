using GestaoUsuarios.Application.Clientes.Queries;
using GestaoUsuarios.Application.Clientes.Response;
using GestaoUsuarios.Domain.Interfaces;
using GestaoUsuarios.Domain.Util;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestaoUsuarios.Application.Clientes.Handlers
{
    public class ObterTodosOsClientesQueryHandler : IRequestHandler<ObterTodosOsClientesQuery, List<ClienteResponse>>
    {
        private readonly IClienteRepository _clienteRepository;
        public ObterTodosOsClientesQueryHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<List<ClienteResponse>> Handle(ObterTodosOsClientesQuery request, CancellationToken cancellationToken)
        {
            List<ClienteResponse> clientesResponse = new();
            var clientes = await _clienteRepository.Include().Where(c => c.Ativo).ToListAsync(cancellationToken);
            foreach (var cliente in clientes)
            {
                clientesResponse.Add(new ClienteResponse
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    PorteEmpresa = EnumExtensions.ObterDescricaoDoEnum(cliente.PorteEmpresa)
                });
            }
            return clientesResponse;
        }
    }
}

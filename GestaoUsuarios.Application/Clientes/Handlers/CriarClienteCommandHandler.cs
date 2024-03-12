using GestaoUsuarios.Application.Clientes.Commands;
using GestaoUsuarios.Domain.Core;
using GestaoUsuarios.Domain.Interfaces;
using GestaoUsuarios.Domain.Entities;
using MediatR;
using FluentValidation.Results;

namespace GestaoUsuarios.Application.Clientes.Handlers
{
    public class CriarClienteCommandHandler : BaseCommandHandler, IRequestHandler<CriarClienteCommand, ValidationResult>
    {
        private readonly IClienteRepository _clienteRepository;
        public CriarClienteCommandHandler(IClienteRepository clienteRepository) : base(clienteRepository.UnitOfWork)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<ValidationResult> Handle(CriarClienteCommand request, CancellationToken cancellationToken)
        {
            var clienteACadastrar = new Cliente
            {
                Nome = request.Nome,
                PorteEmpresa = request.PorteEmpresa
            };
            if (await IsValidAsync(clienteACadastrar))
            {
                await _clienteRepository.CreateAsync(clienteACadastrar, cancellationToken);
                return await CommitAsync(cancellationToken);
            }

            return await RollbackAsync(cancellationToken);
        }
    }
}

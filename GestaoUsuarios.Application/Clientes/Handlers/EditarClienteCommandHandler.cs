using FluentValidation.Results;
using GestaoUsuarios.Application.Clientes.Commands;
using GestaoUsuarios.Domain.Core;
using GestaoUsuarios.Domain.Interfaces;
using MediatR;

namespace GestaoUsuarios.Application.Clientes.Handlers
{
    public class EditarClienteCommandHandler : BaseCommandHandler, IRequestHandler<EditarClienteCommand, ValidationResult>
    {
        private readonly IClienteRepository _clienteRepository;
        public EditarClienteCommandHandler(IClienteRepository clienteRepository) : base(clienteRepository.UnitOfWork)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<ValidationResult> Handle(EditarClienteCommand request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                AddError("O Id do usuário a ser editado é obrigatório e deve ser maior que 0");
                return ResultadoValidacao;
            }
            var clienteAEditar = await _clienteRepository.GetByIdAsync(request.Id, cancellationToken);
            if (clienteAEditar == null)
            {
                AddError("Cliente não encontrado");
                return ResultadoValidacao;
            }
            clienteAEditar.Nome = request.Nome;
            clienteAEditar.PorteEmpresa = request.PorteEmpresa;
            if (await IsValidAsync(clienteAEditar))
            {
                await _clienteRepository.UpdateAsync(clienteAEditar, cancellationToken);
                return await CommitAsync(cancellationToken);
            }

            return await RollbackAsync(cancellationToken);
        }
    }
}

using FluentValidation.Results;
using GestaoUsuarios.Application.Clientes.Commands;
using GestaoUsuarios.Domain.Core;
using GestaoUsuarios.Domain.Interfaces;
using MediatR;

namespace GestaoUsuarios.Application.Clientes.Handlers
{
    public class DesativarClienteCommandHandler : BaseCommandHandler, IRequestHandler<DesativarClienteCommand, ValidationResult>
    {
        private readonly IClienteRepository _clienteRepository;

        public DesativarClienteCommandHandler(IClienteRepository clienteRepository)
            : base(clienteRepository.UnitOfWork)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(DesativarClienteCommand request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                AddError("O Id do cliente a ser excluído é obrigatório e deve ser maior que 0");
                return ResultadoValidacao;
            }
            var clienteAExcluir = await _clienteRepository.GetByIdAsync(request.Id, cancellationToken);
            if (clienteAExcluir == null)
            {
                AddError("Cliente não encontrado");
                return ResultadoValidacao;
            }
            if (!clienteAExcluir.Ativo)
            {
                AddError("Cliente já se encontra desativado");
                return ResultadoValidacao;
            }
            await _clienteRepository.DeleteAsync(clienteAExcluir, cancellationToken);
            return await CommitAsync(cancellationToken);
        }
    }
}
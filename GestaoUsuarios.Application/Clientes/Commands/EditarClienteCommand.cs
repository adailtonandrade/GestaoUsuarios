using GestaoUsuarios.Domain.Core;
using GestaoUsuarios.Domain.Enum;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace GestaoUsuarios.Application.Clientes.Commands
{
    public record EditarClienteCommand : BaseCommand<ValidationResult>
    {
        public string? Nome { get; set; } = null;
        public PorteEmpresaEnum? PorteEmpresa { get; set; } = null;
    }
}

using GestaoUsuarios.Domain.Core;

namespace GestaoUsuarios.Application.Clientes.Commands
{
    public record DesativarClienteCommand : BaseCommand
    {
        public int Id { get; set; }
        public DesativarClienteCommand(int id)
        {
            Id = id;
        }
    }
}

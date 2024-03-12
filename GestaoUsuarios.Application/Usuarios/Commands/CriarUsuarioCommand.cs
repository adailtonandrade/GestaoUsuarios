using GestaoUsuarios.Domain.Core;
using GestaoUsuarios.Domain.Core.Response;
using System.ComponentModel.DataAnnotations;

namespace GestaoUsuarios.Application.Usuarios.Commands
{
    public record CriarUsuarioCommand : BaseCommand<Response<string>>
    {
        public required string NomeUsuario { get; set; }
        public required string NomeExibicao { get; set; }
        public required string Senha { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
    }
}

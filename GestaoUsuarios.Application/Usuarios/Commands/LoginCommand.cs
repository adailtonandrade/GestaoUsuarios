using GestaoUsuarios.Domain.Core.Response;
using GestaoUsuarios.Domain.Core;
using GestaoUsuarios.Application.Usuarios.Response;

namespace GestaoUsuarios.Application.Usuarios.Commands
{
    public record LoginCommand : BaseCommand<Response<UsuarioLoginResponse>>
    {
        public required string NomeUsuario { get; set; }
        public required string Senha { get; set; }
    }
}


namespace GestaoUsuarios.Application.Usuarios.Response
{
    public record UsuarioLoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime DataExpiracao { get; set; }
    }
}

using GestaoUsuarios.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace GestaoUsuarios.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityResult> CreateUserAsync(Usuario usuario, string senha);
        Task<bool> SigninUserAsync(string nomeUsuario, string senha);
        Task<string> GetUserIdAsync(string nomeUsuario);
        Task<Usuario> GetUserByUserNameAsync(string nomeUsuario);
        Task<string> GetUserNameAsync(string idUsuario);
        Task<IdentityResult> DeleteUserAsync(string idUsuario);
        Task<bool> IsUniqueUserName(string nomeUsuario);
        Task<bool> CheckPasswordAsync(Usuario usuario, string senha);
    }
}

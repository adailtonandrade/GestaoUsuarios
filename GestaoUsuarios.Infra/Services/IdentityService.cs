using GestaoUsuarios.Application.Interfaces;
using GestaoUsuarios.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GestaoUsuarios.Infra.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public IdentityService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        // Return multiple value
        public async Task<IdentityResult> CreateUserAsync(Usuario usuario, string senha)
        {
            return await _userManager.CreateAsync(usuario, senha);
        }

        public async Task<IdentityResult> DeleteUserAsync(string idUsuario)
        {
            var usuario = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == idUsuario);
            var result = await _userManager.DeleteAsync(usuario);
            return result;
        }
        public async Task<string> GetUserIdAsync(string nomeUsuario)
        {
            var usuario = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == nomeUsuario);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            return await _userManager.GetUserIdAsync(usuario);
        }

        public async Task<string> GetUserNameAsync(string idUsuario)
        {
            var usuario = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == idUsuario);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            return await _userManager.GetUserNameAsync(usuario);
        }

        public async Task<bool> IsUniqueUserName(string nomeUsuario)
        {
            return await _userManager.FindByNameAsync(nomeUsuario) == null;
        }

        public async Task<bool> SigninUserAsync(string nomeUsuario, string senha)
        {
            var result = await _signInManager.PasswordSignInAsync(nomeUsuario, senha, true, false);
            return result.Succeeded;
        }

        public async Task<Usuario> GetUserByUserNameAsync(string nomeUsuario)
        {
            return await _userManager.FindByNameAsync(nomeUsuario);
        }

        public async Task<bool> CheckPasswordAsync(Usuario usuario, string senha)
        {
            return await _userManager.CheckPasswordAsync(usuario, senha);
        }
    }
}

using GestaoUsuarios.Presentation.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.SessionStorage;
using GestaoUsuarios.Application.Usuarios.Response;
using System.Net.Http.Json;

namespace GestaoUsuarios.Presentation.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpClientFactory _clientFactory;
        private ISessionStorageService _sessionStorageService;

        private const string JWT_KEY = nameof(JWT_KEY);
        //private const string REFRESH_KEY = nameof(REFRESH_KEY);

        private string? _jwtCache;

        public event Action<string?>? LoginChange;

        public AuthenticationService(IHttpClientFactory clientFactory, ISessionStorageService sessionStorageService)
        {
            _clientFactory = clientFactory;
            _sessionStorageService = sessionStorageService;
        }

        public async ValueTask<string> GetJwtAsync()
        {
            if (string.IsNullOrEmpty(_jwtCache))
                _jwtCache = await _sessionStorageService.GetItemAsync<string>(JWT_KEY);

            return _jwtCache;
        }

        public async Task LogoutAsync()
        {
            var response = await _clientFactory.CreateClient("ServerApi").DeleteAsync("api/authentication/revoke");

            await _sessionStorageService.RemoveItemAsync(JWT_KEY);
            //await _sessionStorageService.RemoveItemAsync(REFRESH_KEY);

            _jwtCache = null;

            await Console.Out.WriteLineAsync($"Revogar deu resposta {response.StatusCode}");

            LoginChange?.Invoke(null);
        }

        private static string GetUsername(string token)
        {
            var jwt = new JwtSecurityToken(token);

            return jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value;
        }

        public async Task<DateTime> LoginAsync(LoginModel model)
        {
            var response = await _clientFactory.CreateClient("ServerApi").PostAsync("api/autenticacao/login",
                                                        JsonContent.Create(model));

            if (!response.IsSuccessStatusCode)
                throw new UnauthorizedAccessException("Falha no Login");

            var content = await response.Content.ReadFromJsonAsync<UsuarioLoginResponse>();

            if (content == null)
                throw new InvalidDataException();

            await _sessionStorageService.SetItemAsync(JWT_KEY, content.Token);
            //await _sessionStorageService.SetItemAsync(REFRESH_KEY, content.RefreshToken);

            LoginChange?.Invoke(GetUsername(content.Token));
            return content.DataExpiracao;
        }

        //public async Task<bool> RefreshAsync()
        //{
        //    var model = new RefreshModel
        //    {
        //        AccessToken = await _sessionStorageService.GetItemAsync<string>(JWT_KEY),
        //        RefreshToken = await _sessionStorageService.GetItemAsync<string>(REFRESH_KEY)
        //    };

        //    var response = await _factory.CreateClient("ServerApi").PostAsync("api/authentication/refresh",
        //                                                JsonContent.Create(model));

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        await LogoutAsync();

        //        return false;
        //    }

        //    var content = await response.Content.ReadFromJsonAsync<LoginResponse>();

        //    if (content == null)
        //        throw new InvalidDataException();

        //    await _sessionStorageService.SetItemAsync(JWT_KEY, content.JwtToken);
        //    await _sessionStorageService.SetItemAsync(REFRESH_KEY, content.RefreshToken);

        //    _jwtCache = content.JwtToken;

        //    return true;
        //}
    }
}

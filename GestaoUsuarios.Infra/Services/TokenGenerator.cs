using GestaoUsuarios.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestaoUsuarios.Infra.Services
{
    public class TokenGenerator : ITokenGenerator
    {

        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _expiracaoEmMinutos;

        public TokenGenerator(string key, string issuer, string audience, string expiracaoEmMinutos)
        {
            _key = key;
            _issuer = issuer;
            _audience = audience;
            _expiracaoEmMinutos = expiracaoEmMinutos;
        }

        public (string, DateTime) GerarJWTToken((string idUsuario, string nomeUsuario) detalhesDoUsuario)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, detalhesDoUsuario.nomeUsuario),
                new Claim(JwtRegisteredClaimNames.Jti, detalhesDoUsuario.idUsuario),
                new Claim(ClaimTypes.Name, detalhesDoUsuario.nomeUsuario),
                new Claim("UserId", detalhesDoUsuario.idUsuario)
            };
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_expiracaoEmMinutos)),
                signingCredentials: signingCredentials
           );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return (encodedToken, token.ValidTo);
        }
    }
}

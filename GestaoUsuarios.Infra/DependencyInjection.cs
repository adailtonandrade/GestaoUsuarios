using GestaoUsuarios.Application.Interfaces;
using GestaoUsuarios.Domain.Core.Interfaces;
using GestaoUsuarios.Domain.Identity;
using GestaoUsuarios.Domain.Interfaces;
using GestaoUsuarios.Infra.Context;
using GestaoUsuarios.Infra.Data;
using GestaoUsuarios.Infra.Data.Repository;
using GestaoUsuarios.Infra.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoUsuarios.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var _key = configuration["Jwt:Key"];
            var _issuer = configuration["Jwt:Issuer"];
            var _audience = configuration["Jwt:Audience"];
            var _expiracaoEmMinutos = configuration["Jwt:ExpiracaoEmMinutos"];

            services.AddDbContext<GestaoUsuariosContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<GestaoUsuariosContext>()
                .AddSignInManager()
                .AddErrorDescriber<IdentityPortugueseMessages>()
                .AddDefaultTokenProviders();

            services.AddSingleton<ITokenGenerator>(new TokenGenerator(_key, _issuer, _audience, _expiracaoEmMinutos));

            services.AddScoped<IIdentityService, IdentityService>();
            return services;
        }
    }
}

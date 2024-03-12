using GestaoUsuarios.Domain.Entities;
using GestaoUsuarios.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GestaoUsuarios.Infra.Context
{
    public class GestaoUsuariosContext : IdentityDbContext<Usuario>
    {
        public GestaoUsuariosContext(DbContextOptions<GestaoUsuariosContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}

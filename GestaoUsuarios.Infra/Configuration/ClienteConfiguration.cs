using GestaoUsuarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoUsuarios.Infra.Configuration
{
    internal class ClienteConfiguration : EntityTypeConfiguration<Cliente>
    {
        protected override void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.Property(t => t.Nome)
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder.Property(c => c.PorteEmpresa)
                .IsRequired();
        }
    }
}

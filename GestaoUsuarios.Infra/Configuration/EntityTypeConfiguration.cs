using GestaoUsuarios.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GestaoUsuarios.Infra.Configuration
{
    internal abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : Entity<TEntity>
    {
        protected abstract void Configure(EntityTypeBuilder<TEntity> builder);

        void IEntityTypeConfiguration<TEntity>.Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(typeof(TEntity).Name);
            builder.HasKey(t => t.Id);
            builder.Ignore(c => c.CascadeMode);
            builder.Ignore(c => c.ClassLevelCascadeMode);
            builder.Ignore(c => c.RuleLevelCascadeMode);

            Configure(builder);
        }
    }
}

using GestaoUsuarios.Domain.Core.Interfaces;
using GestaoUsuarios.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace GestaoUsuarios.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GestaoUsuariosContext _gestaoUsuariosContext;
        public UnitOfWork(GestaoUsuariosContext gestaoUsuariosContext)
        {
            _gestaoUsuariosContext = gestaoUsuariosContext;
        }
        public virtual IDbContextTransaction CurrentTransaction => _gestaoUsuariosContext.Database.CurrentTransaction;

        public virtual async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_gestaoUsuariosContext.Database.CurrentTransaction != null)
                return _gestaoUsuariosContext.Database.CurrentTransaction;

            return await _gestaoUsuariosContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public virtual async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_gestaoUsuariosContext.Database.CurrentTransaction != null)
                await _gestaoUsuariosContext.Database.CurrentTransaction.CommitAsync(cancellationToken);
        }

        public bool HasChanges()
        {
            var hasChanges = _gestaoUsuariosContext.ChangeTracker.HasChanges();
            return hasChanges;
        }

        public virtual async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_gestaoUsuariosContext.Database.CurrentTransaction != null)
                await _gestaoUsuariosContext.Database.CurrentTransaction.RollbackAsync(cancellationToken);
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken = default) =>
            _gestaoUsuariosContext.SaveChangesAsync(cancellationToken);

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }
    }
}

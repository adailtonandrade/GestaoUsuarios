using GestaoUsuarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq.Expressions;
using GestaoUsuarios.Infra.Context;
using GestaoUsuarios.Domain.Core.Interfaces;

namespace GestaoUsuarios.Infra.Data.Repository
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected readonly GestaoUsuariosContext Context;
        protected readonly DbSet<TEntity> DbSet;
        private readonly IUnitOfWork _unitOfWork;
        private bool _disposed;

        protected GenericRepository(GestaoUsuariosContext context, IUnitOfWork unitOfWork)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
            _unitOfWork = unitOfWork;
        }

        IUnitOfWork IGenericRepository<TEntity>.UnitOfWork => _unitOfWork;

        public async ValueTask<TEntity> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await Include().SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async ValueTask<List<TEntity>> GetAsync(CancellationToken cancellationToken = default)
        {
            return await Include().ToListAsync(cancellationToken);
        }

        public async ValueTask<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await Include().Where(predicate).ToListAsync(cancellationToken);
        }

        public async ValueTask<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbSet.AnyAsync(predicate, cancellationToken);
        }

        public async ValueTask<bool> ExistsAsync<TParameter>(Expression<Func<TParameter, bool>> predicate, CancellationToken cancellationToken = default) where TParameter : class
        {
            return await Context.Set<TParameter>().AnyAsync(predicate, cancellationToken);
        }

        public ValueTask<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.DataCriacao = DateTime.Now;
            entity.Ativo = true;
            var entityEntry = DbSet.Add(entity);
            return new ValueTask<TEntity>(entityEntry.Entity);
        }

        public ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = DbSet.Update(entity);
            return new ValueTask<TEntity>(entityEntry.Entity);
        }

        public ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.Ativo = false;
            return new ValueTask();
        }

        public ValueTask DeleteAsync<TParameter>(TParameter entity, CancellationToken cancellationToken = default) where TParameter : Entity<TParameter>
        {
            entity.Ativo = false;
            return new ValueTask();
        }

        public async ValueTask DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            var entity = DbSet.Find(id);
            entity.Ativo = false;
            await SaveChangesAsync(cancellationToken);
        }

        public virtual IQueryable<TEntity> Include()
        {
            return DbSet;
        }

        public DbConnection GetDbConnection() =>
            Context.Database.GetDbConnection();

        protected virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            Context.SaveChangesAsync(cancellationToken);

        protected Task BeginTransactionAsync(CancellationToken cancellationToken = default) =>
            Context.Database.BeginTransactionAsync(cancellationToken);

        protected Task CommitTransactionAsync(CancellationToken cancellationToken = default) =>
            Context.Database.CommitTransactionAsync(cancellationToken);

        protected Task RollbackTransactionAsync(CancellationToken cancellationToken = default) =>
            Context.Database.RollbackTransactionAsync(cancellationToken);

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                Context.Dispose();

            _disposed = true;
        }

        #endregion IDisposable
    }
}

using GestaoUsuarios.Domain.Entities;
using System.Data.Common;
using System.Linq.Expressions;


namespace GestaoUsuarios.Domain.Core.Interfaces
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        IUnitOfWork UnitOfWork { get; }

        ValueTask<TEntity> GetByIdAsync(long id, CancellationToken cancellationToken = default);

        ValueTask<List<TEntity>> GetAsync(CancellationToken cancellationToken = default);

        ValueTask<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        ValueTask<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        ValueTask<bool> ExistsAsync<TParameter>(Expression<Func<TParameter, bool>> predicate, CancellationToken cancellationToken = default) where TParameter : class;

        ValueTask<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        ValueTask DeleteAsync<TParameter>(TParameter entity, CancellationToken cancellationToken = default) where TParameter : Entity<TParameter>;

        ValueTask DeleteAsync(long id, CancellationToken cancellationToken = default);
        IQueryable<TEntity> Include();
        DbConnection GetDbConnection();
    }
}

using Microsoft.EntityFrameworkCore.Storage;

namespace GestaoUsuarios.Domain.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IDbContextTransaction CurrentTransaction { get; }

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

        Task<int> SaveAsync(CancellationToken cancellationToken = default);

        bool HasChanges();
    }
}

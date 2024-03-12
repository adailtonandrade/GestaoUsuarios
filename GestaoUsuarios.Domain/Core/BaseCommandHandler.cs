using FluentValidation.Results;
using GestaoUsuarios.Domain.Core.Interfaces;
using GestaoUsuarios.Domain.Core.Response;
using GestaoUsuarios.Domain.Entities;

namespace GestaoUsuarios.Domain.Core
{
    public abstract class BaseCommandHandler
    {
        private readonly IUnitOfWork _uow;
        protected ValidationResult ResultadoValidacao { get; } = new ValidationResult();

        protected BaseCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected async Task<bool> IsValidAsync<TParameter>(TParameter target) where TParameter : Entity<TParameter>
        {
            await target.IsValidAsync();
            foreach (var error in target.ResultadoValidacao.Errors)
                ResultadoValidacao.Errors.Add(error);

            return ResultadoValidacao.IsValid;
        }

        protected bool IsSuccess(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                ResultadoValidacao.Errors.Add(error);

            return ResultadoValidacao.IsValid;
        }

        protected void AddError(string mensagem)
        {
            ResultadoValidacao.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected Task BeginTransactionAsync(CancellationToken cancellationToken = default) =>
            _uow.BeginTransactionAsync(cancellationToken);

        protected Task CommitTransactionAsync(CancellationToken cancellationToken = default) =>
            _uow.CommitTransactionAsync(cancellationToken);

        protected Task<int> SaveAsync(CancellationToken cancellationToken = default) =>
            _uow.SaveAsync(cancellationToken);

        protected async Task<ValidationResult> CommitAsync(string message, CancellationToken cancellationToken = default)
        {
            if (_uow.HasChanges() && (await _uow.SaveAsync(cancellationToken) <= 0))
                AddError(message);

            return ResultadoValidacao;
        }

        protected async Task<ValidationResult> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await CommitAsync("Erro ao salvar os dados", cancellationToken);
        }

        protected async Task<ValidationResult> RollbackAsync(CancellationToken cancellationToken = default)
        {
            await _uow.RollbackTransactionAsync(cancellationToken);
            return ResultadoValidacao;
        }

        protected bool HasChanges()
        {
            return _uow.HasChanges();
        }

        protected static Response<TData> Success<TData>(TData data, ValidationResult resultadoValidacao = default)
        {
            return Response<TData>.Success(data, resultadoValidacao);
        }

        protected static Response<TData> Fail<TData>(ValidationResult resultadoValidacao = default)
        {
            return Response<TData>.Fail(resultadoValidacao);
        }

        protected static TEntity Remover<TEntity>(TEntity entity)
            where TEntity : Entity<TEntity>
        {
            entity.Ativo = false;
            return entity;
        }

        protected static List<TEntity> Remover<TEntity>(List<TEntity> entities)
            where TEntity : Entity<TEntity>
        {
            return entities.Select(x =>
            {
                x = Remover(x);
                return x;
            }).ToList();
        }
    }
}
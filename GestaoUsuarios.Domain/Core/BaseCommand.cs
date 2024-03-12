using FluentValidation.Results;
using MediatR;


namespace GestaoUsuarios.Domain.Core
{
    public abstract record BaseCommand<TResponse> : IRequest<TResponse>
    {
        public int Id { get; set; }
    }

    public abstract record BaseCommand : BaseCommand<ValidationResult>
    {
    }
}

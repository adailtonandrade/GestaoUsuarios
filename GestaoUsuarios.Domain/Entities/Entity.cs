using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace GestaoUsuarios.Domain.Entities
{
    public abstract class Entity<T> : AbstractValidator<T> 
            where T : Entity<T>
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public bool Ativo { get; set; }

        [NotMapped]
        [JsonIgnore]
        public ValidationResult ResultadoValidacao { get; protected set; } = new ValidationResult();

        public virtual Task<bool> IsValidAsync() => Task.FromResult(true);
    }
}

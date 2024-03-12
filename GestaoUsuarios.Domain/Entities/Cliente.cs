using FluentValidation;
using GestaoUsuarios.Domain.Enum;


namespace GestaoUsuarios.Domain.Entities
{
    public class Cliente : Entity<Cliente>
    {
        public string Nome { get; set; }

        public PorteEmpresaEnum? PorteEmpresa { get; set; }
        public override async Task<bool> IsValidAsync()
        {
            RuleFor(c => c.Nome)
                .NotNull().NotEmpty().WithMessage("Nome obrigatório")
                .MaximumLength(200).WithMessage("O nome não pode ter mais de 200 caracteres");

            RuleFor(c => c.PorteEmpresa)
                .IsInEnum().WithMessage("O porte da Empresa deve estar entre 0 e 2, 0 - Pequena, 1 - Média, 2 - Grande")
                .NotNull().WithMessage("É obrigatório definir um porte para empresa");

            ResultadoValidacao = await ValidateAsync(this);
            return ResultadoValidacao.IsValid;
        }
    }
}


using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GestaoUsuarios.Domain.Identity
{
    public class Usuario : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string NomeExibicao { get; set; } = string.Empty;
    }
}

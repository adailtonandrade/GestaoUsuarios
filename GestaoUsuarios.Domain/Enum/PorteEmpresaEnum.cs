using System.ComponentModel;


namespace GestaoUsuarios.Domain.Enum
{
    public enum PorteEmpresaEnum
    {
        [Description("Pequeno Porte")]
        PequenoPorte = 1,
        [Description("Médio Porte")]
        MedioPorte = 2,
        [Description("Grande Porte")]
        GrandePorte = 3
    }
}

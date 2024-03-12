using System.ComponentModel;
using System.Reflection;

namespace GestaoUsuarios.Infra.Util
{
    public static class EnumExtensions
    {
        public static string ObterDescricaoDoEnum(this Enum valor)
        {
            FieldInfo fi = valor.GetType().GetField(valor.ToString());
            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return valor.ToString();
        }
    }
}
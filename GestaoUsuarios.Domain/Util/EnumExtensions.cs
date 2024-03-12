using System.ComponentModel;
using System.Reflection;

namespace GestaoUsuarios.Domain.Util
{
    public static class EnumExtensions
    {
        public static string ObterDescricaoDoEnum<T>(T valor)
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
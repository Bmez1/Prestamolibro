using PruebaIngresoblibliotecario.Core.utils.Enums;
using System.Collections.Generic;

namespace PruebaIngresoblibliotecario.Core.utils
{
    public class DiasPrestamo
    {
        public static IDictionary<TipoUsuario, int> DiasPrestamoByUsuario { get; } = new Dictionary<TipoUsuario, int>()
        {
            { TipoUsuario.AFILIADO, 10 },
            { TipoUsuario.EMPLEADO, 8 },
            { TipoUsuario.INVITADO, 7 }
        };
    }
}
using PruebaIngresoblibliotecario.Core.utils.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace PruebaIngresoblibliotecario.Core.Validations
{
    public class ExisteTipoUsuario : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Enum.IsDefined(typeof(TipoUsuario), value);
        }
    }
}
using PruebaIngresoblibliotecario.Core.utils.Enums;
using PruebaIngresoblibliotecario.Core.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace PruebaIngresoblibliotecario.Core.DTOs.Inputs
{
    public class CrearPrestamoDto
    {
        [Required(ErrorMessage = "El ISBN es obligatorio")]
        public Guid Isbn { get; set; }

        [Required, MaxLength(10, ErrorMessage = "La identificación debe poseer máximo 10 digitos")]
        public string IdentificacionUsuario { get; set; }

        [Required, ExisteTipoUsuario(ErrorMessage = "El tipo de usuario no existe")]
        public TipoUsuario TipoUsuario { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace PruebaIngresoblibliotecario.Core.DTOs.Inputs
{
    public class DevolverPrestamoInput
    {
        [Required(ErrorMessage = "El ISBN es obligatorio")]
        public Guid Isbn { get; set; }

        [Required, MaxLength(10, ErrorMessage = "La identificación debe poseer máximo 10 digitos")]
        public string IdentificacionUsuario { get; set; }
    }
}
using PruebaIngresoblibliotecario.Core.utils.Enums;
using PruebaIngresoblibliotecario.Core.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace PruebaIngresoblibliotecario.Core.Entities
{
    public class Prestamo
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid Isbn { get; set; }

        [Required, MaxLength(10, ErrorMessage = "La identificación debe poseer máximo 10 digitos")]
        public string IdentificacionUsuario { get; set; }

        public DateTime FechaMaximaDevolucion { get; set; }

        [Required, ExisteTipoUsuario(ErrorMessage = "El tipo de usuario no existe")]
        public TipoUsuario TipoUsuario { get; set; }

        public bool IsDevuelto { get; set; } = false;
        public DateTime FechaPrestamo { get; private set; } = DateTime.Now;
    }
}
using PruebaIngresoblibliotecario.Core.utils.Enums;
using System;

namespace PruebaIngresoblibliotecario.Core.DTOs
{
    public class DataPrestamoDto
    {
        public Guid Id { get; set; }
        public Guid Isbn { get; set; }
        public string IdentificacionUsuario { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public DateTime FechaMaximaDevolucion { get; set; }
    }
}

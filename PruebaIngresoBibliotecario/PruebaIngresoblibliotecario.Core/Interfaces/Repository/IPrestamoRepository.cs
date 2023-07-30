using PruebaIngresoblibliotecario.Core.DTOs.Inputs;
using PruebaIngresoblibliotecario.Core.Entities;
using System.Threading.Tasks;

namespace PruebaIngresoblibliotecario.Core.Interfaces.Repository
{
    public interface IPrestamoRepository : IBaseRepository<Prestamo>
    {
        /// <summary>
        /// Finalidad: Verifica si un usuario tiene un prestamo activo
        /// </summary>
        /// <param name="identificacion"></param>
        /// <returns>true si existe un prestamo devuelto o no</returns>
        Task<bool> PrestamoDevuleto(string identificacion);

        /// <summary>
        /// Finalidad: Verifica si existe un prestamo dada una identifiación de usuario
        /// y un ISBN del libro
        /// </summary>
        /// <param name="prestamo"></param>
        /// <returns>true si existe un prestamo activo o no</returns>
        Task<Prestamo> GetPrestamoFindByISBN_Identificacion(DevolverPrestamoInput prestamo);
    }
}
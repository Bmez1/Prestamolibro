using Microsoft.EntityFrameworkCore;
using PruebaIngresoBibliotecario.Infraestructura.Context;
using PruebaIngresoblibliotecario.Core.DTOs.Inputs;
using PruebaIngresoblibliotecario.Core.Entities;
using PruebaIngresoblibliotecario.Core.Interfaces.Repository;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Infraestructura.Repositories
{
    public class Prestamorepository : BaseRepository<Prestamo>, IPrestamoRepository
    {
        private readonly PersistenceContext _context;

        public Prestamorepository(PersistenceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Prestamo> GetPrestamoFindByISBN_Identificacion(DevolverPrestamoInput prestamo)
            => await _context.Prestamos.FirstOrDefaultAsync(x => x.IdentificacionUsuario == prestamo.IdentificacionUsuario.Trim() &&
            x.Isbn == prestamo.Isbn &&
            !x.IsDevuelto);

        public async Task<bool> PrestamoDevuleto(string identificacion)
            => await _context.Prestamos.AnyAsync(x => x.IdentificacionUsuario == identificacion.Trim() && x.IsDevuelto);
    }
}
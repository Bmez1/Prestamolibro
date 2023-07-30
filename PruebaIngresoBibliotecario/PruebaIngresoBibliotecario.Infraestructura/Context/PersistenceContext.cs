using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PruebaIngresoblibliotecario.Core.Entities;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Infraestructura.Context
{
    public class PersistenceContext : DbContext
    {
        private readonly IConfiguration Config;

        public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration config) : base(options)
        {
            Config = config;
        }

        public DbSet<Prestamo> Prestamos { get; set; }

        public async Task CommitAsync()
        {
            await SaveChangesAsync().ConfigureAwait(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Config.GetValue<string>("SchemaName"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
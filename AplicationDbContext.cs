using Microsoft.EntityFrameworkCore;
using WebApiAuthores.Controllers.Entidades;

namespace WebApiAuthores
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // Aca permitimos usar Querys a las tablas, si no esta aca, no podes hacer un Select ni nada derecho en la Tabla X
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}

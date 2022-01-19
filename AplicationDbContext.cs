using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiAuthores.Controllers.Entidades;

namespace WebApiAuthores
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<AutorLibro>()
        //       .HasKey(al => new { al.AutorId, al.LibroID });
        //}

        // Aca permitimos usar Querys a las tablas, si no esta aca, no podes hacer un Select ni nada derecho en la Tabla X
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
    }
}

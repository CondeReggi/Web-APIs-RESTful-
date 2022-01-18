using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAuthores.Controllers.Entidades;

namespace WebApiAuthores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly AplicationDbContext context;
        public LibrosController(AplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            return await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);   
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existeAutorAsociado = await context.Autores.AnyAsync(x => x.Id == libro.AutorId); //Buscamos que exista el Autor con el id posteado en Libro

            if (!existeAutorAsociado)
            {
                return BadRequest("No existe el autor");  // StatusCode(StatusCodes.Status400BadRequest, "No hay autor asociado al libro!");
            }

            //try
            //{
                context.Add(libro);
                await context.SaveChangesAsync();
                return Ok();
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, "Se produjo un error inesperado");
            //}
        }

    }
}

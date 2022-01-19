
using WebApiAuthores.DTOs;
using WebApiAuthores.Controllers.Entidades;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("prueba")]
        public Task<ActionResult> ObtenerPrueba()
        {
            return Ok();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            try
            {
                return await context.Libros.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
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

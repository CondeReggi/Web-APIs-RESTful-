using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAuthores.DTOs;
using AutoMapper;
using WebApiAuthores.Controllers.Entidades;

namespace WebApiAuthores.Controllers
{
    [ApiController]
    [Route("api/libros/{libroid: int}/comentarios")]
    public class ComentariosController : ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;

        public ComentariosController(AplicationDbContext context, IMapper mapper )
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(int libroid, ComentarioCreacionDTO creacionComentarioDTO)
        {
            var existeLibro = await context.Libros.AnyAsync(x => x.Id == libroid);

            if (!existeLibro)
            {
                return NotFound();
            }

            var comentario = mapper.Map<Comentario>(creacionComentarioDTO);
            comentario.LibroId = libroid;
            context.Add(comentario);

            await context.SaveChangesAsync();  
            return Ok(comentario);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAuthores.Controllers.Entidades;

namespace WebApiAuthores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AuthoresController : ControllerBase
    {
        private readonly AplicationDbContext context;
        public AuthoresController(AplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.ToListAsync();
        }

        [HttpPost] // ESTO ES LA URL es decir lo de arriba del todo osea api/autores
        public async Task<ActionResult> Post(Autor autor)
        {
            //Hay que validar que exita el usuario o no

            context.Add(autor); //Tengo que agregar el body con el .Add
            await context.SaveChangesAsync();    //Aca guardo los datos del contexto en la base de datos
            return Ok();    //Status 200
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> GetAutor(int id)
        {
            try
            {
                var Autor = await context.Autores.Where(a => a.Id == id).FirstOrDefaultAsync(); // Siempre referencia a context => Autores 

                return Autor == null ? NotFound() : Ok(Autor);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Autor autor)
        {
            try
            {
                if (id != autor.Id)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, $"Autor whit id: { id } does not exist ");
                }

                context.Update(autor); // Toma abosolutamente todo lo del body y lo cambia con el autor pasado
                await context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, "Changes was succesfull");
            }
            catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al uptadear la data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeAutor = await context.Autores.AnyAsync(x => x.Id == id); // Ture or false

            //Console.WriteLine(existeAutor);

            if (!existeAutor)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Autor does not exist");
            }

            context.Remove( new Autor() { Id = id }  );
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

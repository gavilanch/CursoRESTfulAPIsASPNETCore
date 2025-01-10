using BibliotecaAPI.Datos;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<AutoresController> logger;

        public AutoresController(ApplicationDbContext context, ILogger<AutoresController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet("/listado-de-autores")] // /listado-de-autores
        [HttpGet] // api/autores
        public async Task<IEnumerable<Autor>> Get()
        {
            logger.LogTrace("Obteniendo el listado de autores");
            //logger.LogDebug("Obteniendo el listado de autores");
            //logger.LogInformation("Obteniendo el listado de autores");
            //logger.LogWarning("Obteniendo el listado de autores");
            //logger.LogError("Obteniendo el listado de autores");
            //logger.LogCritical("Obteniendo el listado de autores");
            return await context.Autores.ToListAsync();
        }

        [HttpGet("primero")] // api/autores/primero
        public async Task<Autor> GetPrimerAutor()
        {
            return await context.Autores.FirstAsync();
        }

        [HttpGet("{id:int}")] // api/autores/id?incluirLibros=true|false
        public async Task<ActionResult<Autor>> Get([FromRoute] int id, [FromHeader] bool incluirLibros)
        {
            var autor = await context.Autores
                .Include(x => x.Libros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (autor is null)
            {
                return NotFound();
            }

            return autor;
        }

        [HttpGet("{nombre:alpha}")]
        public async Task<IEnumerable<Autor>> Get(string nombre)
        {
            return await context.Autores.Where(x => x.Nombre.Contains(nombre)).ToListAsync();
        }

        //[HttpGet("{parametro1}/{parametro2?}")] // api/autores/felipe/gavilan
        //public ActionResult Get(string parametro1, string parametro2 = "valor por defecto")
        //{
        //    return Ok(new { parametro1, parametro2 });
        //}

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] // api/autores/id
        public async Task<ActionResult> Put(int id, Autor autor)
        {
            if (id != autor.Id)
            {
                return BadRequest("Los ids deben de coincidir");
            }

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Autores.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                return NotFound(); 
            }
             
            return Ok();
        }
    }
}

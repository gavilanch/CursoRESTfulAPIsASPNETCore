using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiModulo4.Contexts;
using WebApiModulo4.Entities;
using WebApiModulo4.Helpers;
using WebApiModulo4.Services;

namespace WebApiModulo4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IClaseB claseB;

        public AutoresController(ApplicationDbContext context, ClaseB claseB)
        {
            this.context = context;
            this.claseB = claseB;
        }

        // GET api/autores
        [HttpGet("/listado")]
        [HttpGet("listado")]
        [HttpGet]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            //throw new NotImplementedException();
            claseB.HacerAlgo();
            return context.Autores.ToList();
        }

        [HttpGet("Primer")]
        public ActionResult<Autor> GetPrimerAutor()
        {
            return context.Autores.FirstOrDefault();
        }

        // GET api/autores/5 
        // GET api/autores/5/felipe
        [HttpGet("{id}/{param2=Gavilan}")]
        public async Task<ActionResult<Autor>> Get(int id, string param2)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        // POST api/autores
        [HttpPost]
        public ActionResult Post([FromBody] Autor autor)
        {
            context.Add(autor);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autor);
        }

        // PUT api/autores/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Autor author)
        {
            context.Entry(author).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        // DELETE api/autores/5
        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var autor = context.Autores.FirstOrDefault(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            context.Remove(autor);
            context.SaveChanges();
            return Ok(autor);
        }

    }
}

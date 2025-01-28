using AutoMapper;
using BibliotecaAPI.Datos;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/libros")]
    [Authorize(Policy = "esadmin")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ITimeLimitedDataProtector protectorLimitadoPorTiempo;

        public LibrosController(ApplicationDbContext context, IMapper mapper,
            IDataProtectionProvider protectionProvider)
        {
            this.context = context;
            this.mapper = mapper;
            protectorLimitadoPorTiempo = protectionProvider
                    .CreateProtector("LibrosController").ToTimeLimitedDataProtector();
        }

        [HttpGet("listado/obtener-token")]
        public ActionResult ObtenerTokenListado()
        {
            var textoPlano = Guid.NewGuid().ToString();
            var token = protectorLimitadoPorTiempo.Protect(textoPlano, lifetime: TimeSpan.FromSeconds(30));
            var url = Url.RouteUrl("ObtenerListadoLibrosUsandoToken", new { token }, "https");
            return Ok(new { url });
        }

        [HttpGet("listado/{token}", Name = "ObtenerListadoLibrosUsandoToken")]
        [AllowAnonymous]
        public async Task<ActionResult> ObtenerListadoUsandoToken(string token)
        {
            try
            {
                protectorLimitadoPorTiempo.Unprotect(token);
            } 
            catch 
            {
                ModelState.AddModelError(nameof(token), "El token ha expirado");
                return ValidationProblem();
            }

            var libros = await context.Libros.ToListAsync();
            var librosDTO = mapper.Map<IEnumerable<LibroDTO>>(libros);
            return Ok(librosDTO);
        }

        [HttpGet]
        public async Task<IEnumerable<LibroDTO>> Get()
        {
            var libros = await context.Libros.ToListAsync();
            var librosDTO = mapper.Map<IEnumerable<LibroDTO>>(libros);
            return librosDTO;
        }

        [HttpGet("{id:int}", Name = "ObtenerLibro")]
        public async Task<ActionResult<LibroConAutoresDTO>> Get(int id)
        {
            var libro = await context.Libros
                .Include(x => x.Autores)
                    .ThenInclude(x => x.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (libro is null)
            {
                return NotFound();
            }

            var libroDTO = mapper.Map<LibroConAutoresDTO>(libro);

            return libroDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroCreacionDTO)
        {
            if (libroCreacionDTO.AutoresIds is null || libroCreacionDTO.AutoresIds.Count == 0)
            {
                ModelState.AddModelError(nameof(libroCreacionDTO.AutoresIds),
                    "No se puede crear un libro sin autores");
                return ValidationProblem();
            }

            var autoresIdsExisten = await context.Autores
                                    .Where(x => libroCreacionDTO.AutoresIds.Contains(x.Id))
                                    .Select(x => x.Id).ToListAsync();

            if (autoresIdsExisten.Count != libroCreacionDTO.AutoresIds.Count)
            {
                var autoresNoExisten = libroCreacionDTO.AutoresIds.Except(autoresIdsExisten);
                var autoresNoExistenString = string.Join(",", autoresNoExisten);
                var mensajeDeError = $"Los siguientes autores no existen: {autoresNoExistenString}";
                ModelState.AddModelError(nameof(libroCreacionDTO.AutoresIds), mensajeDeError);
                return ValidationProblem();
            }

            var libro = mapper.Map<Libro>(libroCreacionDTO);
            AsignarOrdenAutores(libro);
            
            context.Add(libro);
            await context.SaveChangesAsync();

            var libroDTO = mapper.Map<LibroDTO>(libro);

            return CreatedAtRoute("ObtenerLibro", new { id = libro.Id }, libroDTO);
        }

        private void AsignarOrdenAutores(Libro libro)
        {
            if (libro.Autores is not null)
            {
                for (int i = 0; i < libro.Autores.Count; i++)
                {
                    libro.Autores[i].Orden = i;
                }
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, LibroCreacionDTO libroCreacionDTO)
        {
            if (libroCreacionDTO.AutoresIds is null || libroCreacionDTO.AutoresIds.Count == 0)
            {
                ModelState.AddModelError(nameof(libroCreacionDTO.AutoresIds),
                    "No se puede crear un libro sin autores");
                return ValidationProblem();
            }

            var autoresIdsExisten = await context.Autores
                                    .Where(x => libroCreacionDTO.AutoresIds.Contains(x.Id))
                                    .Select(x => x.Id).ToListAsync();

            if (autoresIdsExisten.Count != libroCreacionDTO.AutoresIds.Count)
            {
                var autoresNoExisten = libroCreacionDTO.AutoresIds.Except(autoresIdsExisten);
                var autoresNoExistenString = string.Join(",", autoresNoExisten);
                var mensajeDeError = $"Los siguientes autores no existen: {autoresNoExistenString}";
                ModelState.AddModelError(nameof(libroCreacionDTO.AutoresIds), mensajeDeError);
                return ValidationProblem();
            }

            var libroDB = await context.Libros
                            .Include(x => x.Autores)
                            .FirstOrDefaultAsync(x => x.Id == id);

            if (libroDB is null)
            {
                return NotFound();
            }

            libroDB = mapper.Map(libroCreacionDTO, libroDB);
            AsignarOrdenAutores(libroDB);
          
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Libros.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

using BibliotecaAPI.Datos;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;
using BibliotecaAPI.Servicios;
using BibliotecaAPI.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers.V1
{
    [ApiController]
    [Route("api/v1/restriccionesdominio")]
    [Authorize]
    [DeshabilitarLimitarPeticiones]
    public class RestriccionesDominioController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IServiciosUsuarios serviciosUsuarios;

        public RestriccionesDominioController(ApplicationDbContext context,
            IServiciosUsuarios serviciosUsuarios)
        {
            this.context = context;
            this.serviciosUsuarios = serviciosUsuarios;
        }

        [HttpPost]
        public async Task<ActionResult> Post(RestriccionDominioCreacionDTO restriccionDominioCreacionDTO)
        {
            var llaveDB = await context.LlavesAPI.FirstOrDefaultAsync(x => x.Id ==
            restriccionDominioCreacionDTO.LlaveId);

            if (llaveDB is null)
            {
                return NotFound();
            }

            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            if (llaveDB.UsuarioId != usuarioId)
            {
                return Forbid();
            }

            var restriccionDominio = new RestriccionDominio
            {
                LlaveId = restriccionDominioCreacionDTO.LlaveId,
                Dominio = restriccionDominioCreacionDTO.Dominio
            };

            context.Add(restriccionDominio);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Post(int id, 
            RestriccionDominioActualizacionDTO restriccionDominioActualizacionDTO)
        {
            var restriccionDB = await context.RestriccionesDominio.Include(x => x.Llave)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (restriccionDB is null)
            {
                return NotFound();
            }

            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            if (restriccionDB.Llave!.UsuarioId != usuarioId)
            {
                return Forbid();
            }

            restriccionDB.Dominio = restriccionDominioActualizacionDTO.Dominio;

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var restriccionDB = await context.RestriccionesDominio.Include(x => x.Llave)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (restriccionDB is null)
            {
                return NotFound();
            }

            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            if (restriccionDB.Llave!.UsuarioId != usuarioId)
            {
                return Forbid();
            }

            context.Remove(restriccionDB);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}

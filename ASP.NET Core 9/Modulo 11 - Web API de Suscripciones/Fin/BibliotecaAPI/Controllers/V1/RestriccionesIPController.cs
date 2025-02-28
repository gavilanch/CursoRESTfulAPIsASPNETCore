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
    [Route("api/v1/restriccionesip")]
    [Authorize]
    [DeshabilitarLimitarPeticiones]
    public class RestriccionesIPController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IServiciosUsuarios serviciosUsuarios;

        public RestriccionesIPController(ApplicationDbContext context, 
            IServiciosUsuarios serviciosUsuarios)
        {
            this.context = context;
            this.serviciosUsuarios = serviciosUsuarios;
        }

        [HttpPost]
        public async Task<ActionResult> Post(RestriccionIPCreacionDTO restriccionIPCreacionDTO)
        {
            var llaveDB = await context.LlavesAPI
                .FirstOrDefaultAsync(x => x.Id == restriccionIPCreacionDTO.LlaveId);

            if (llaveDB is null)
            {
                return NotFound();
            }

            var usuarioId = serviciosUsuarios.ObtenerUsuarioId();

            if (llaveDB.UsuarioId != usuarioId)
            {
                return Forbid();
            }

            var restriccionIP = new RestriccionIP
            {
                LlaveId = restriccionIPCreacionDTO.LlaveId,
                IP = restriccionIPCreacionDTO.IP
            };

            context.Add(restriccionIP);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,
            RestriccionIPActualizacionDTO restriccionIPActualizacionDTO)
        {
            var restriccionDB = await context.RestriccionesIP
                .Include(x => x.Llave)
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

            restriccionDB.IP = restriccionIPActualizacionDTO.IP;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var restriccionDB = await context.RestriccionesIP
                .Include(x => x.Llave)
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

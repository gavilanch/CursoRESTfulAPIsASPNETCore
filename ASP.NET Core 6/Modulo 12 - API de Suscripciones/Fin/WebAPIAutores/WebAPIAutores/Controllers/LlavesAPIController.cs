
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAutores.DTOs;
using WebAPIAutores.Servicios;

namespace WebAPIAutores.Controllers;
[ApiController]
[Route("api/llavesapi")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class LlavesAPIController: CustomBaseController
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private readonly ServicioLlaves servicioLlaves;

    public LlavesAPIController(ApplicationDbContext context,
        IMapper mapper, ServicioLlaves servicioLlaves)
    {
        this.context = context;
        this.mapper = mapper;
        this.servicioLlaves = servicioLlaves;
    }

    [HttpGet]
    public async Task<List<LlaveDTO>> MisLlaves()
    {
        var usuarioId = ObtenerUsuarioId();
        var llaves = await context.LlavesAPI
            .Include(x => x.RestriccionesDominio)
            .Include(x => x.RestriccionesIP)
            .Where(x => x.UsuarioId == usuarioId).ToListAsync();
        return mapper.Map<List<LlaveDTO>>(llaves);
    }

    [HttpPost]
    public async Task<ActionResult> CrearLlave(CrearLlaveDTO crearLlaveDTO)
    {
        var usuarioId = ObtenerUsuarioId();

        if (crearLlaveDTO.TipoLlave == Entidades.TipoLlave.Gratuita)
        {
            var elUsuarioYaTieneUnaLlaveGratuita = await context.LlavesAPI
                .AnyAsync(x => x.UsuarioId == usuarioId && x.TipoLlave == Entidades.TipoLlave.Gratuita);

            if (elUsuarioYaTieneUnaLlaveGratuita)
            {
                return BadRequest("El usuario ya tiene una llave gratuita");
            }
        }

        await servicioLlaves.CrearLlave(usuarioId, crearLlaveDTO.TipoLlave);
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> ActualizarLlave(ActualizarLlaveDTO actualizarLlaveDTO)
    {
        var usuarioId = ObtenerUsuarioId();

        var llaveDB = await context.LlavesAPI.FirstOrDefaultAsync(x => x.Id == actualizarLlaveDTO.LlaveId);

        if (llaveDB == null)
        {
            return NotFound();
        }

        if (usuarioId != llaveDB.UsuarioId)
        {
            return Forbid();
        }

        if (actualizarLlaveDTO.ActualizarLlave)
        {
            llaveDB.Llave = servicioLlaves.GenerarLlave();
        }

        llaveDB.Activa = actualizarLlaveDTO.Activa;
        await context.SaveChangesAsync();
        return NoContent();
    }
}

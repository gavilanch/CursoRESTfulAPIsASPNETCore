using BibliotecaAPI.Datos;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers.V1
{
    [Route("api/v1/facturas")]
    [ApiController]
    [Authorize]
    [DeshabilitarLimitarPeticiones]
    public class FacturasController: ControllerBase
    {
        private readonly ApplicationDbContext context;

        public FacturasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Pagar(FacturaPagarDTO facturaPagarDTO)
        {
            var facturaDB = await context.Facturas
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == facturaPagarDTO.FacturaId);

            if (facturaDB is null)
            {
                return NotFound();
            }

            if (facturaDB.Pagada)
            {
                ModelState.AddModelError(nameof(facturaPagarDTO.FacturaId), "La factura ya fue saldada");
                return ValidationProblem();
            }

            // Pretender que el pago fue exitoso

            facturaDB.Pagada = true;
            await context.SaveChangesAsync();

            var hayFacturasPendientesVencidas = await context.Facturas
                .AnyAsync(x => x.UsuarioId == facturaDB.UsuarioId &&
                !x.Pagada && x.FechaLimiteDePago < DateTime.Today);

            if (!hayFacturasPendientesVencidas)
            {
                facturaDB.Usuario!.MalaPaga = false;
                await context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}

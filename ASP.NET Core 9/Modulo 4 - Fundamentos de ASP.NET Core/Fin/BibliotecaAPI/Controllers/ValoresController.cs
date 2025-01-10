using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/valores")]
    public class ValoresController : ControllerBase
    {
        private readonly IRepositorioValores repositorioValores;
        private readonly ServicioTransient transient1;
        private readonly ServicioTransient transient2;
        private readonly ServicioScoped scoped1;
        private readonly ServicioScoped scoped2;
        private readonly ServicioSingleton singleton;

        public ValoresController(IRepositorioValores repositorioValores,
            ServicioTransient transient1,
            ServicioTransient transient2,
            ServicioScoped scoped1,
            ServicioScoped scoped2,
            ServicioSingleton singleton
            )
        {
            this.repositorioValores = repositorioValores;
            this.transient1 = transient1;
            this.transient2 = transient2;
            this.scoped1 = scoped1;
            this.scoped2 = scoped2;
            this.singleton = singleton;
        }

        [HttpGet("servicios-tiempo-de-vida")]
        public IActionResult GetServiciosTiempoDeVida()
        {
            return Ok(new
            {
                Transients = new
                {
                    transient1 = transient1.ObtenerGuid,
                    transient2 = transient2.ObtenerGuid
                },
                Scopeds = new
                {
                    scoped1 = scoped1.ObtenerGuid,
                    scoped2 = scoped2.ObtenerGuid
                },
                Singleton = singleton.ObtenerGuid
            });
        }

        [HttpGet]
        public IEnumerable<Valor> Get()
        {
            return repositorioValores.ObtenerValores();
        }

        [HttpPost]
        public IActionResult Post(Valor valor)
        {
            repositorioValores.InsertarValor(valor);
            return Ok();
        }
    }
}

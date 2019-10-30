using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiModulo9.Entities;
using WebApiModulo9.Services;

namespace WebApiModulo9.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoresController: ControllerBase
    {
        private readonly IRepositorioAutores repositorioAutores;

        public AutoresController(IRepositorioAutores repositorioAutores)
        {
            this.repositorioAutores = repositorioAutores;
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Autor> Get(int id)
        {

            var autor = repositorioAutores.ObtenerPorId(id);

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }
    }
}

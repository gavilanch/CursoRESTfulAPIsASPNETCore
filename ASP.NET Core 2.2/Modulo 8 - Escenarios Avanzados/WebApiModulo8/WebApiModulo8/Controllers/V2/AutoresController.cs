using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiModulo8.Contexts;
using WebApiModulo8.Entities;
using WebApiModulo8.Helpers;

namespace WebApiModulo8.Controllers.V2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    //[HttpHeaderIsPresent("x-version", "2")]
    public class AutoresController: ControllerBase
    {
        private ApplicationDbContext context;

        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet(Name = "ObtenerAutoresV2")]
        [ServiceFilter(typeof(HATEOASAuthorsFilterAttribute))]
        public async Task<ActionResult<IEnumerable<Autor>>> Get()
        {
            var autores = await context.Autores.ToListAsync();
            return autores;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiModulo8.Models;

namespace WebApiModulo8.Controllers.V1
{
    [ApiController]
    [Route("api/v1")]
    public class RootController: ControllerBase
    {
        private readonly IUrlHelper _urlHelper;

        public RootController(IUrlHelper urlHelper)
        {
            this._urlHelper = urlHelper;
        }

        [HttpGet(Name = "GetRoot")]
        public ActionResult<IEnumerable<Enlace>> Get()
        {
            List<Enlace> enlaces = new List<Enlace>();

            // Aquí colocamos los links
            enlaces.Add(new Enlace(href: _urlHelper.Link("GetRoot", new { }), rel: "self", metodo: "GET"));
            enlaces.Add(new Enlace(href: _urlHelper.Link("ObtenerAutores", new { }), rel: "autores", metodo: "GET"));
            enlaces.Add(new Enlace(href: _urlHelper.Link("CrearAutor", new { }), rel: "crear-autor", metodo: "POST"));
            enlaces.Add(new Enlace(href: _urlHelper.Link("ObtenerValores", new { }), rel: "valores", metodo: "GET"));
            enlaces.Add(new Enlace(href: _urlHelper.Link("CrearValor", new { }), rel: "crear-valor", metodo: "POST"));

            return enlaces;
        }
    }
}

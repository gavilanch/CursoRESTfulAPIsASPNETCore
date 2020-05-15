using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiModulo8.Models;

namespace WebApiModulo8.Helpers
{
    public class GeneradorEnlaces
    {
        private readonly IUrlHelperFactory urlHelperFactory;
        private readonly IActionContextAccessor actionContextAccessor;

        public GeneradorEnlaces(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
        {
            this.urlHelperFactory = urlHelperFactory;
            this.actionContextAccessor = actionContextAccessor;
        }

        private IUrlHelper ConstruirURLHelper()
        {
            return urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public ColeccionDeRecursos<AutorDTO> GenerarEnlaces(List<AutorDTO> autores)
        {
            var _urlHelper = ConstruirURLHelper();
            var resultado = new ColeccionDeRecursos<AutorDTO>(autores);
            autores.ForEach(a => GenerarEnlaces(a));
            resultado.Enlaces.Add(new Enlace(_urlHelper.Link("ObtenerAutores", new { }), rel: "self", metodo: "GET"));
            resultado.Enlaces.Add(new Enlace(_urlHelper.Link("CrearAutor", new { }), rel: "crear-autor", metodo: "POST"));
            return resultado;
        }

        public void GenerarEnlaces(AutorDTO autor)
        {
            var _urlHelper = ConstruirURLHelper();
            autor.Enlaces.Add(new Enlace(_urlHelper.Link("ObtenerAutor", new { id = autor.Id }), rel: "self", metodo: "GET"));
            autor.Enlaces.Add(new Enlace(_urlHelper.Link("ActualizarAutor", new { id = autor.Id }), rel: "actualizar-autor", metodo: "PUT"));
            autor.Enlaces.Add(new Enlace(_urlHelper.Link("BorrarAutor", new { id = autor.Id }), rel: "borrar-autor", metodo: "DELETE"));
        }
    }

}

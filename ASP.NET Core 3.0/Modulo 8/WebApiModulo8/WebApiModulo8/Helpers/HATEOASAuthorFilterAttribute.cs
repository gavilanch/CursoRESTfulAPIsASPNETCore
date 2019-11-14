using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiModulo8.Models;

namespace WebApiModulo8.Helpers
{
    public class HATEOASAuthorFilterAttribute : HATEOASFilterAttribute
    {
        private readonly GeneradorEnlaces generadorEnlaces;

        public HATEOASAuthorFilterAttribute(GeneradorEnlaces generadorEnlaces)
        {
            this.generadorEnlaces = generadorEnlaces ?? throw new ArgumentNullException(nameof(generadorEnlaces));
        }
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var incluirHATEOAS = DebeIncluirHATEOAS(context);

            if (!incluirHATEOAS)
            {
                await next();
                return;
            }

            var result = context.Result as ObjectResult;
            var model = result.Value as AutorDTO;
            if (model == null)
            {
                var modelList = result.Value as List<AutorDTO> ?? throw new ArgumentNullException("Se esperaba una instancia de AutorDTO");
                result.Value = generadorEnlaces.GenerarEnlaces(modelList);
                await next();
            }
            else
            {
                generadorEnlaces.GenerarEnlaces(model);
                await next();
            }

        }
    }

}

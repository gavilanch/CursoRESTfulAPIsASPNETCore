using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiModulo8.Helpers
{
    public class HATEOASFilterAttribute : ResultFilterAttribute
    {
        protected bool DebeIncluirHATEOAS(ResultExecutingContext context)
        {
            var result = context.Result as ObjectResult;
            if (!EsRespuestaExitosa(result))
            {
                return false;
            }

            var header = context.HttpContext.Request.Headers["IncluirHATEOAS"];

            if (header.Count == 0)
            {
                return false;
            }

            var accept = header[0];

            if (!accept.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            return true;
        }
        private bool EsRespuestaExitosa(ObjectResult result)
        {
            if (result == null || result.Value == null)
            {
                return false;
            }

            if (result.StatusCode.HasValue && !result.StatusCode.Value.ToString().StartsWith("2"))
            {
                return false;
            }

            return true;
        }
    }

}

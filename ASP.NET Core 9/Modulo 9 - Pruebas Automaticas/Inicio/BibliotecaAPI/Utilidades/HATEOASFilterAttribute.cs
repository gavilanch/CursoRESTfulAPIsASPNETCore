using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BibliotecaAPI.Utilidades
{
    public class HATEOASFilterAttribute : ResultFilterAttribute
    {
        protected bool DebeIncluirHATEOAS(ResultExecutingContext context)
        {
            if (context.Result is not ObjectResult result || !EsRespuestaExitosa(result))
            {
                return false;
            }

            if (!context.HttpContext.Request.Headers.TryGetValue("IncluirHATEOAS", 
                    out var cabecera))
            {
                return false;
            }

            return string.Equals(cabecera, "Y", StringComparison.OrdinalIgnoreCase);
        }

        private bool EsRespuestaExitosa(ObjectResult result)
        {
            if (result.Value is null)
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

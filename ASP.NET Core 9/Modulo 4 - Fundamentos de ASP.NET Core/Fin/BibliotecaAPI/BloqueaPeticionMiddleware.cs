namespace BibliotecaAPI
{
    public class BloqueaPeticionMiddleware
    {
        private readonly RequestDelegate next;

        public BloqueaPeticionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext contexto)
        {
            if (contexto.Request.Path == "/bloqueado")
            {
                contexto.Response.StatusCode = 403;
                await contexto.Response.WriteAsync("Acceso denegado");
            }
            else
            {
                await next.Invoke(contexto);
            }
        }
    }

    public static class BloqueaPeticionMiddlewareExtensions
    {
        public static IApplicationBuilder UseBloqueaPeticion(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BloqueaPeticionMiddleware>();
        }
    }
}

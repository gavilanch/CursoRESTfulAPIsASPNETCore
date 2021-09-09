
using WebAPIAutores.Entidades;

namespace WebAPIAutores.Servicios;
public class ServicioLlaves
{
    private readonly ApplicationDbContext context;

    public ServicioLlaves(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task CrearLlave(string usuarioId, TipoLlave tipoLlave)
    {
        var llave = GenerarLlave();

        var llaveAPI = new LlaveAPI
        {
            Activa = true,
            Llave = llave,
            TipoLlave = tipoLlave,
            UsuarioId = usuarioId
        };

        context.Add(llaveAPI);
        await context.SaveChangesAsync();
    }

    public string GenerarLlave()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }
}

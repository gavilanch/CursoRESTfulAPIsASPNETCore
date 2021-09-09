
namespace WebAPIAutores.Entidades;
public class RestriccionDominio
{
    public int Id { get; set; }
    public int LlaveId { get; set; }
    public string Dominio { get; set; }
    public LlaveAPI Llave { get; set; }
}

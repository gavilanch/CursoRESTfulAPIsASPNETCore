
namespace WebAPIAutores.Entidades;
public class Peticion
{
    public int Id { get; set; }
    public int LlaveId { get; set; }
    public DateTime FechaPeticion { get; set; }
    public LlaveAPI Llave { get; set; }
}

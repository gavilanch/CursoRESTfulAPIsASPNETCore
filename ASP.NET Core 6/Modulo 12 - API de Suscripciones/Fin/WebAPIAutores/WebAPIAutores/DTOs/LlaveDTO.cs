
namespace WebAPIAutores.DTOs;
public class LlaveDTO
{
    public int Id { get; set; }
    public string Llave { get; set; }
    public bool Activa { get; set; }
    public string TipoLlave { get; set; }
    public List<RestriccionDominioDTO> RestriccionesDominio { get; set; }
    public List<RestriccionIPDTO> RestriccionesIP { get; set; }
}

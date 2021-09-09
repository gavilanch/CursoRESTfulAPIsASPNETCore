
using System.ComponentModel.DataAnnotations;

namespace WebAPIAutores.DTOs;
public class ActualizarRestriccionDominioDTO
{
    [Required]
    public string Dominio { get; set; }
}


using System.ComponentModel.DataAnnotations;

namespace WebAPIAutores.DTOs;
public class ActualizarRestriccionIPDTO
{
    [Required]
    public string IP { get; set; }
}

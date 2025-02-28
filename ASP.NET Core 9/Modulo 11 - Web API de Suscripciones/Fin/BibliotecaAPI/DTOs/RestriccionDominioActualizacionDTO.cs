using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class RestriccionDominioActualizacionDTO
    {
        [Required]
        public required string Dominio { get; set; }
    }
}

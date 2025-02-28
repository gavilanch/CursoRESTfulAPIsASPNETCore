using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class RestriccionIPActualizacionDTO
    {
        [Required]
        public required string IP { get; set; }
    }
}

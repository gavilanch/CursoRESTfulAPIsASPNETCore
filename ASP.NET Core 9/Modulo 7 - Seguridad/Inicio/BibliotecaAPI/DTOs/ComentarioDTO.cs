using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class ComentarioDTO
    {
        public Guid Id { get; set; }
        public required string Cuerpo { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}

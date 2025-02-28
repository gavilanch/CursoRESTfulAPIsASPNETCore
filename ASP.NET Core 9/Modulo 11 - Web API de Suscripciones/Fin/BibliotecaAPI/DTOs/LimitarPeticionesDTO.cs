using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class LimitarPeticionesDTO
    {
        public const string Seccion = "limitarPeticiones";
        [Required]
        public int PeticionesPorDiaGratuito { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class EditarClaimDTO
    {
        [EmailAddress]
        [Required]
        public required string Email { get; set; }
    }
}

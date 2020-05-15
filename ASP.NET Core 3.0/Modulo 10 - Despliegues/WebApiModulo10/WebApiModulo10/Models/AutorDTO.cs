using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiModulo10.Models
{
    public class AutorDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<LibroDTO> Books { get; set; }
    }
}

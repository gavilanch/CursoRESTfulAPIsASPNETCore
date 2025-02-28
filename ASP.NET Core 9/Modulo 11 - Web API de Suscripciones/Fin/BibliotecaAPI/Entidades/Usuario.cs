using Microsoft.AspNetCore.Identity;

namespace BibliotecaAPI.Entidades
{
    public class Usuario: IdentityUser
    {
        public DateTime FechaNacimiento { get; set; }
        public bool MalaPaga { get; set; }
    }
}

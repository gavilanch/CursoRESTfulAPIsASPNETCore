using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Entidades
{
    [PrimaryKey("Mes", "Año")]
    public class FacturaEmitida
    {
        public int Mes { get; set; }
        public int Año { get; set; }
    }
}

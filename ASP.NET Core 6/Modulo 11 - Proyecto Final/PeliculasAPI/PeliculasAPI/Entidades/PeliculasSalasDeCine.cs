using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.Entidades
{
    public class PeliculasSalasDeCine
    {
        public int PeliculaId { get; set; }
        public int SalaDeCineId { get; set; }
        public Pelicula Pelicula { get; set; }
        public SalaDeCine SalaDeCine { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs
{
    public class SalaDeCineCercanoDTO: SalaDeCineDTO
    {
        public double DistanciaEnMetros { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiModulo4.Services
{
    public class ClaseA
    {
        private readonly IClaseB claseB;
        public ClaseA(IClaseB claseB)
        {
            this.claseB = claseB;
        }

        public void RealizarAccion()
        {
            claseB.HacerAlgo();
        }

    }
}

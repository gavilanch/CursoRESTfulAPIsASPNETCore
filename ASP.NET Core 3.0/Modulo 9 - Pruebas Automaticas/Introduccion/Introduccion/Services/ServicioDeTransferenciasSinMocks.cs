using Introduccion.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Introduccion.Services
{
    public class ServicioDeTransferenciasSinMocks
    {
        public void TransferirEntreCuentas(Cuenta origen, Cuenta destino, decimal montoATransferir)
        {
            if (montoATransferir > origen.Fondos)
            {
                throw new ApplicationException("La cuenta origen no tiene fondos suficientes para realizar la operación");
            }

            origen.Fondos -= montoATransferir;
            destino.Fondos += montoATransferir;
        }
    }
}

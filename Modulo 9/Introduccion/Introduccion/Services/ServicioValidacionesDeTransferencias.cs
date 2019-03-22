using Introduccion.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Introduccion.Services
{
    public class ServicioValidacionesDeTransferencias : IServicioValidacionesDeTransferencias
    {
        public string RealizarValidaciones(Cuenta origen, Cuenta destino, decimal montoATransferir)
        {
            if (montoATransferir > origen.Fondos)
            {
                return "La cuenta origen no tiene fondos suficientes para realizar la operación";
            }

            // ... otras validaciones

            return string.Empty;
        }
    }
}

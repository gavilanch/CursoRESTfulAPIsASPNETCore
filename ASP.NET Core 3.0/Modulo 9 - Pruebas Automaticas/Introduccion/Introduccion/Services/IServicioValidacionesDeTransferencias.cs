using Introduccion.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Introduccion.Services
{
    public interface IServicioValidacionesDeTransferencias
    {
        string RealizarValidaciones(Cuenta origen, Cuenta destino, decimal montoATransferir);
    }
}

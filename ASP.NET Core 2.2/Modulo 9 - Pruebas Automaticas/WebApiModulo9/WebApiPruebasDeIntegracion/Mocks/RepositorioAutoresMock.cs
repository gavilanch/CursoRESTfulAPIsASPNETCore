using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiModulo9.Entities;
using WebApiModulo9.Services;

namespace WebApiPruebasDeIntegracion.Mocks
{
    public class RepositorioAutoresMock : IRepositorioAutores
    {
        public Autor ObtenerPorId(int id)
        {
            if (id == 0)
            {
                return null;
            }

            return new Autor()
            {
                Id = id,
                Nombre = "Claudia Rodríguez"
            };
        }
    }
}

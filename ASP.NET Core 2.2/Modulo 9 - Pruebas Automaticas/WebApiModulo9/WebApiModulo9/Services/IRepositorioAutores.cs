using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiModulo9.Entities;

namespace WebApiModulo9.Services
{
    public interface IRepositorioAutores
    {
        Autor ObtenerPorId(int id);
    }
}

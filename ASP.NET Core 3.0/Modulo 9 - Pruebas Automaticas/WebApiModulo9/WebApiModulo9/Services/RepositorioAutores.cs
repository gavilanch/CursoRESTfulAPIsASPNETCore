using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiModulo9.Contexts;
using WebApiModulo9.Entities;

namespace WebApiModulo9.Services
{
    public class RepositorioAutores : IRepositorioAutores
    {
        private readonly ApplicationDbContext context;

        public RepositorioAutores(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Autor ObtenerPorId(int id)
        {
            return context.Autores.FirstOrDefault(x => x.Id == id);
        }
    }
}

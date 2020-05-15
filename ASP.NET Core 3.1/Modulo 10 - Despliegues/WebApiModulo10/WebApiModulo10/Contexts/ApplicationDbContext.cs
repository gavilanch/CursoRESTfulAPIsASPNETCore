using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiModulo10.Entities;

namespace WebApiModulo10.Contexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        public DbSet<Autor> Autores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var autores = new List<Autor>()
            {
                new Autor(){Id = 1, Nombre = "Felipe Gavilán", FechaNacimiento = new DateTime(1900, 2, 5) },
                new Autor(){Id = 2, Nombre = "Claudia Rodríguez", FechaNacimiento = new DateTime(1905, 4, 15) }
            };

            modelBuilder.Entity<Autor>().HasData(autores);

            var libros = new List<Libro>()
            {
                new Libro(){Id = 1, Titulo = "Entity Framework Core 2.1 - De verdad", AutorId = 1},
                new Libro(){Id = 2, Titulo = "Construyendo Web APIs con ASP.NET Core 2.2", AutorId = 2},
                new Libro(){Id = 3, Titulo = "Libro de prueba", AutorId = 2}
            };

            modelBuilder.Entity<Libro>().HasData(libros);

            base.OnModelCreating(modelBuilder);
        }

    }
}

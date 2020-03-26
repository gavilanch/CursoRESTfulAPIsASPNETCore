using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiModulo4.Entities;

namespace WebApiModulo4.Contexts
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
                new Autor(){Id = 1, Nombre = "Felipe Gavilán" },
                new Autor(){Id = 2, Nombre = "Claudia Rodríguez" }
            };

            modelBuilder.Entity<Autor>().HasData(autores);

            base.OnModelCreating(modelBuilder);
        }
    }
}

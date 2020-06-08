using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculasActores>()
                .HasKey(x => new { x.ActorId, x.PeliculaId });

            modelBuilder.Entity<PeliculasGeneros>()
                .HasKey(x => new { x.GeneroId, x.PeliculaId });

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var aventura = new Genero() { Id = 4, Nombre = "Aventura" };
            var animation = new Genero() { Id = 5, Nombre = "Animación" };
            var suspenso = new Genero() { Id = 6, Nombre = "Suspenso" };
            var romance = new Genero() { Id = 7, Nombre = "Romance" };

            modelBuilder.Entity<Genero>()
                .HasData(new List<Genero>
                {
                    aventura, animation, suspenso, romance
                });

            var jimCarrey = new Actor() { Id = 5, Nombre = "Jim Carrey", FechaNacimiento = new DateTime(1962, 01, 17) };
            var robertDowney = new Actor() { Id = 6, Nombre = "Robert Downey Jr.", FechaNacimiento = new DateTime(1965, 4, 4) };
            var chrisEvans = new Actor() { Id = 7, Nombre = "Chris Evans", FechaNacimiento = new DateTime(1981, 06, 13) };

            modelBuilder.Entity<Actor>()
                .HasData(new List<Actor>
                {
                    jimCarrey, robertDowney, chrisEvans
                });

            var endgame = new Pelicula()
            {
                Id = 2,
                Titulo = "Avengers: Endgame",
                EnCines = true,
                FechaEstreno = new DateTime(2019, 04, 26)
            };

            var iw = new Pelicula()
            {
                Id = 3,
                Titulo = "Avengers: Infinity Wars",
                EnCines = false,
                FechaEstreno = new DateTime(2019, 04, 26)
            };

            var sonic = new Pelicula()
            {
                Id = 4,
                Titulo = "Sonic the Hedgehog",
                EnCines = false,
                FechaEstreno = new DateTime(2020, 02, 28)
            };
            var emma = new Pelicula()
            {
                Id = 5,
                Titulo = "Emma",
                EnCines = false,
                FechaEstreno = new DateTime(2020, 02, 21)
            };
            var wonderwoman = new Pelicula()
            {
                Id = 6,
                Titulo = "Wonder Woman 1984",
                EnCines = false,
                FechaEstreno = new DateTime(2020, 08, 14)
            };

            modelBuilder.Entity<Pelicula>()
                .HasData(new List<Pelicula>
                {
                    endgame, iw, sonic, emma, wonderwoman
                });

            modelBuilder.Entity<PeliculasGeneros>().HasData(
                new List<PeliculasGeneros>()
                {
                    new PeliculasGeneros(){PeliculaId = endgame.Id, GeneroId = suspenso.Id},
                    new PeliculasGeneros(){PeliculaId = endgame.Id, GeneroId = aventura.Id},
                    new PeliculasGeneros(){PeliculaId = iw.Id, GeneroId = suspenso.Id},
                    new PeliculasGeneros(){PeliculaId = iw.Id, GeneroId = aventura.Id},
                    new PeliculasGeneros(){PeliculaId = sonic.Id, GeneroId = aventura.Id},
                    new PeliculasGeneros(){PeliculaId = emma.Id, GeneroId = suspenso.Id},
                    new PeliculasGeneros(){PeliculaId = emma.Id, GeneroId = romance.Id},
                    new PeliculasGeneros(){PeliculaId = wonderwoman.Id, GeneroId = suspenso.Id},
                    new PeliculasGeneros(){PeliculaId = wonderwoman.Id, GeneroId = aventura.Id},
                });

            modelBuilder.Entity<PeliculasActores>().HasData(
                new List<PeliculasActores>()
                {
                    new PeliculasActores(){PeliculaId = endgame.Id, ActorId = robertDowney.Id, Personaje = "Tony Stark", Orden = 1},
                    new PeliculasActores(){PeliculaId = endgame.Id, ActorId = chrisEvans.Id, Personaje = "Steve Rogers", Orden = 2},
                    new PeliculasActores(){PeliculaId = iw.Id, ActorId = robertDowney.Id, Personaje = "Tony Stark", Orden = 1},
                    new PeliculasActores(){PeliculaId = iw.Id, ActorId = chrisEvans.Id, Personaje = "Steve Rogers", Orden = 2},
                    new PeliculasActores(){PeliculaId = sonic.Id, ActorId = jimCarrey.Id, Personaje = "Dr. Ivo Robotnik", Orden = 1}
                });
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<PeliculasActores> PeliculasActores { get; set; }
        public DbSet<PeliculasGeneros> PeliculasGeneros { get; set; }
    }
}

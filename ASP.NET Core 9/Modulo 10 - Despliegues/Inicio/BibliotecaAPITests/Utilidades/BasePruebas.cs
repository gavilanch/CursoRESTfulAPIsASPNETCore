using AutoMapper;
using BibliotecaAPI.Datos;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BibliotecaAPITests.Utilidades
{
    public class BasePruebas
    {
        protected readonly JsonSerializerOptions jsonSerializerOptions
            = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        protected readonly Claim adminClaim = new Claim("esadmin", "1");

        protected ApplicationDbContext ConstruirContext(string nombreBD)
        {
            var opciones = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(nombreBD).Options;

            var dbContext = new ApplicationDbContext(opciones);
            return dbContext;
        }

        protected IMapper ConfigurarAutoMapper()
        {
            var config = new MapperConfiguration(opciones =>
            {
                opciones.AddProfile(new AutoMapperProfiles());
            });

            return config.CreateMapper();
        }

        protected WebApplicationFactory<Program> ConstruirWebApplicationFactory(string nombreBD,
            bool ignorarSeguridad = true)
        {
            var factory = new WebApplicationFactory<Program>();

            factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    ServiceDescriptor descriptorDBContext = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IDbContextOptionsConfiguration<ApplicationDbContext>))!;

                    if (descriptorDBContext is not null)
                    {
                        services.Remove(descriptorDBContext);
                    }

                    services.AddDbContext<ApplicationDbContext>(opciones =>
                        opciones.UseInMemoryDatabase(nombreBD));

                    if (ignorarSeguridad)
                    {
                        services.AddSingleton<IAuthorizationHandler, AllowAnonymousHandler>();

                        services.AddControllers(opciones =>
                        {
                            opciones.Filters.Add(new UsuarioFalsoFiltro());
                        });
                    }

                });
            });

            return factory;
        }

        protected async Task<string> CrearUsuario(string nombreBD, WebApplicationFactory<Program> factory)
            => await CrearUsuario(nombreBD, factory, [], "ejemplo@hotmail.com");

        protected async Task<string> CrearUsuario(string nombreBD, WebApplicationFactory<Program> factory,
            IEnumerable<Claim> claims)
            => await CrearUsuario(nombreBD, factory, claims, "ejemplo@hotmail.com");

        protected async Task<string> CrearUsuario(string nombreBD, WebApplicationFactory<Program> factory,
            IEnumerable<Claim> claims, string email)
        {
            var urlRegistro = "/api/v1/usuarios/registro";
            string token = string.Empty;
            token = await ObtenerToken(email, urlRegistro, factory);

            if (claims.Any())
            {
                var context = ConstruirContext(nombreBD);
                var usuario = await context.Users.Where(x => x.Email == email).FirstAsync();
                Assert.IsNotNull(usuario);

                var userClaims = claims.Select(x => new IdentityUserClaim<string>
                {
                    UserId = usuario.Id,
                    ClaimType = x.Type,
                    ClaimValue = x.Value
                });

                context.UserClaims.AddRange(userClaims);
                await context.SaveChangesAsync();
                var urlLogin = "/api/v1/usuarios/login";
                token = await ObtenerToken(email, urlLogin, factory);
            }

            return token;
        }

        private async Task<string> ObtenerToken(string email, string url,
            WebApplicationFactory<Program> factory)
        {
            var password = "aA123456!";
            var credenciales = new CredencialesUsuarioDTO { Email = email, Password = password };
            var cliente = factory.CreateClient();
            var respuesta = await cliente.PostAsJsonAsync(url, credenciales);
            respuesta.EnsureSuccessStatusCode();

            var contenido = await respuesta.Content.ReadAsStringAsync();
            var respuestaAutenticacion = JsonSerializer.Deserialize<RespuestaAutenticacionDTO>(contenido,
                jsonSerializerOptions)!;

            Assert.IsNotNull(respuestaAutenticacion.Token);

            return respuestaAutenticacion.Token;
        }
    }
}

using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiModulo9;
using WebApiModulo9.Entities;
using WebApiModulo9.Services;
using WebApiPruebasDeIntegracion.Filters;
using WebApiPruebasDeIntegracion.Mocks;

namespace WebApiPruebasDeIntegracion
{
    [TestClass]
    public class AutoresControllerTests
    {
        private WebApplicationFactory<Startup> _factory;

        [TestInitialize]
        public void Initialize()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        public WebApplicationFactory<Startup> ConstruirWebHostBuilderConfigurado()
        {
            return _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddMvc(options =>
                    {
                        options.Filters.Add(new AllowAnonymousFilter());
                        options.Filters.Add(new FakeUserFilter());
                    });

                    services.AddScoped<IRepositorioAutores, RepositorioAutoresMock>();
                });
            });
        }

        [TestMethod]
        public async Task Get_SiElAutorNoExiste_Retorna404()
        {
            var client = ConstruirWebHostBuilderConfigurado().CreateClient();

            var url = "/api/autores/0";
            var response = await client.GetAsync(url);

            Assert.AreEqual(expected: 404, actual: (int)response.StatusCode);
        }

        [TestMethod]
        public async Task Get_SiElAutorExiste_EntoncesLoDevuelve()
        {
            var client = ConstruirWebHostBuilderConfigurado().CreateClient();

            var url = "/api/autores/7";
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Assert.IsTrue(false, "Codigo de estatus no exitoso: " + response.StatusCode);
            }

            var result = JsonConvert.DeserializeObject<Autor>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(result);
            Assert.AreEqual(expected: 7, actual: result.Id);
            Assert.AreEqual(expected: "Claudia Rodríguez", actual: result.Nombre);
        }
    }
}

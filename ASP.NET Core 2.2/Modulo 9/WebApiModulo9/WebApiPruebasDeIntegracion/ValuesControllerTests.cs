using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WebApiModulo9;

namespace WebApiPruebasDeIntegracion
{
    [TestClass]
    public class ValuesControllerTests
    {
        private WebApplicationFactory<Startup> _factory;

        [TestInitialize]
        public void Initialize()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [TestMethod]
        public async Task Get_DevuelveArregloDeDosElementos()
        {
            var client = _factory.CreateClient();
            var url = "/api/values";
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Assert.IsTrue(false, "Código de estatus no exitoso: " + response.StatusCode);
            }

            var result = JsonConvert.DeserializeObject<string[]>(
                await response.Content.ReadAsStringAsync());
            Assert.AreEqual(expected: 2, actual: result.Length);
        }
    }
}

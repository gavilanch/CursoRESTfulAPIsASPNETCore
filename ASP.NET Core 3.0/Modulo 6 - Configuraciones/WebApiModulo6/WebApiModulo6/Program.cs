using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApiModulo6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((env, config) =>
            {
                // aquí colocamos la configuración de proveedores 
                var ambiente = env.HostingEnvironment.EnvironmentName;
                config.AddJsonFile($"appsettings.{ambiente}.json", optional: true, reloadOnChange: true);
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();

                if (args != null)
                {
                    config.AddCommandLine(args);
                }

                var currentConfig = config.Build();

                // Estas configuraciones se deben guardar fuera del código fuente del App.
                //config.AddAzureKeyVault(currentConfig["Vault"],
                //    currentConfig["ClientId"],
                //    currentConfig["ClientSecret"]);
            })
                .UseStartup<Startup>();
    }
}

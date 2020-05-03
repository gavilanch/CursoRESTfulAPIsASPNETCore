using hostedService.Contexts;
using hostedService.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace hostedService.Services
{
    public class ConsumeScopedService : IHostedService, IDisposable
    {
        public IServiceProvider Services { get; }
        private Timer timer;

        public ConsumeScopedService(IServiceProvider services)
        {
            Services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void DoWork(object status)
        {
            using (var scope = Services.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                string message = $"ConsumeScopedService: Mensaje generado {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}";
                HostedServiceLog log = new HostedServiceLog() { Message = message };
                context.HostedServiceLogs.Add(log);
                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

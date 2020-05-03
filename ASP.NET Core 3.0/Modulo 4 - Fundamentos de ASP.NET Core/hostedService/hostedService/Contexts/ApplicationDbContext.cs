using hostedService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hostedService.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<HostedServiceLog> HostedServiceLogs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}

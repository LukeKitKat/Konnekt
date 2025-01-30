using Konnect.Service.DatabaseManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnect.Service.DatabaseManager
{
    public class KonnektContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public KonnektContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=KonnektDevelopment;Trusted_Connection=true;TrustServerCertificate=true;Encrypt=false;ConnectRetryCount=0");
        }
    }
}

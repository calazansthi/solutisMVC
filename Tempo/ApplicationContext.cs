using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tempo.Models;

namespace Tempo
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Cidade> Cidade { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {            
            if (!options.IsConfigured)
            {
                //string connectionString = Configuration.GetConnectionString("Default");
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Tempo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }


        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cidade>().HasKey(pk => pk.Id);
        }
    }
}

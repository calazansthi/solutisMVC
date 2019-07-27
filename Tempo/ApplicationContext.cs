using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tempo.Models;

namespace Tempo
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Cidade> Cidades { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cidade>().HasKey(pk => pk.Id);
        }
    }
}

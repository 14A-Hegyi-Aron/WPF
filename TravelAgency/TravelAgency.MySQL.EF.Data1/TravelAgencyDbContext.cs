using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Data.Models;

namespace TravelAgency.MySQL.EF.Data
{
    public class TravelAgencyDbContext : DbContext
    {
        public DbSet<ApplyModel> Applies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=travelAgency;uid=root;pwd=;",
                ServerVersion.Create(10, 4, 24, ServerType.MariaDb)
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HotelModel>().HasIndex(h => h.Name).IsUnique();

            modelBuilder.Entity<TravelModeModel>().HasData(
                new TravelModeModel { Id = 1, Name = "busz" },
                new TravelModeModel { Id = 2, Name = "repülő" },
                new TravelModeModel { Id = 5, Name = "hajó" }
            );
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.MySQL.EF.Data.Tests
{
    public class TestDbContext : TravelAgencyDbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    Random rand = new Random();
        //    optionsBuilder.UseMySql($"server=localhost;database=travels_{rand.Next(int.MaxValue)};uid=root;pwd=;",
        //        ServerVersion.Create(new Version(10, 4, 24), ServerType.MySql));
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Random rand = new Random();
            optionsBuilder.UseInMemoryDatabase($"travels_{rand.Next(int.MaxValue)}");
        }
    }
}

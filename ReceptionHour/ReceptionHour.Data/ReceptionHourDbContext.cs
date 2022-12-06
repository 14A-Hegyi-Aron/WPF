using Microsoft.EntityFrameworkCore;
using ReceptionHour.Data.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptionHour.Data
{
    //Parancsok a Package manager console-ban:
    //  Add-Migration <név> -Project <data project> -StartupProject <data project>
    //  Script-Migration -Project <data project> -StartupProject <data project>
    //  update-database -Project <data project> -StartupProject <data project>

    public class ReceptionHourDbContext : DbContext
    {
        public DbSet<MeetingModel> Meetings { get; set; }

        private readonly string connStr;
        public ReceptionHourDbContext()
        {
            connStr = ConfigurationManager.ConnectionStrings["db"]?.ConnectionString;
#if DEBUG
            if (connStr == null)
                connStr = "server=localhost;database=receptionhour;uid=root;pwd=;";
#endif
        }

        public ReceptionHourDbContext(DbContextOptions<ReceptionHourDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!string.IsNullOrEmpty(connStr))
                optionsBuilder.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeacherModel>().HasIndex(t => t.Name).IsUnique();

            modelBuilder.Entity<TeacherModel>().HasData
            (
                new TeacherModel() { Id = 1, Name = "Teszt Elek", Room = "501", Capacity = 15 },
                new TeacherModel() { Id = 2, Name = "Gipsz Jakab", Room = "502", Capacity = 5 },
                new TeacherModel() { Id = 3, Name = "Winch Eszter", Room = "503", Capacity = 10 }
            );
        }
    }
}

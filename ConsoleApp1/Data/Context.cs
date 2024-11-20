using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Data
{
    public class Context : DbContext
    {
        public Context()
        {
            //fyzické vytvoření souboru doatabáze SQLite - pokud neexistuje
            Database.EnsureCreated();
        }

        // Množina (Set) pro jednotlivé objekty pro ORM
        public DbSet<Student> Students { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // vytvoření souboru + cesty k souboru, kam ukládat data
            optionsBuilder.UseSqlite("Data Source = MySQLDatabase.db");
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // explicitní nastavení vazby 1:N včetně kaskádového mazaní záznamů
            modelBuilder.Entity<Classroom>()
                .HasMany(x => x.Students)
                .WithOne(x => x.Clsroom)
                .HasForeignKey(x => x.ClassroomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

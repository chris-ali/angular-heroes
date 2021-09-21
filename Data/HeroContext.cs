using System;
using angular_heroes.Models;
using Microsoft.EntityFrameworkCore;

namespace angular_heroes.Data
{
    public class HeroContext : DbContext
    {
        public HeroContext(DbContextOptions<HeroContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Hero>().HasData(new Hero { id = 11, name = "Dr Nice", createdBy = "chris", createdDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { id = 12, name = "Narco", createdBy = "chris", createdDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { id = 13, name = "Bombasto", createdBy = "chris", createdDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { id = 14, name = "Celeritas", createdBy = "chris", createdDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { id = 15, name = "Magneta", createdBy = "chris", createdDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { id = 16, name = "RubberMan", createdBy = "chris", createdDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { id = 17, name = "Dynama", createdBy = "chris", createdDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { id = 18, name = "Dr IQ", createdBy = "chris", createdDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { id = 19, name = "Magma", createdBy = "chris", createdDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { id = 20, name = "Tornado", createdBy = "chris", createdDate = DateTime.Now });
        }      

        public DbSet<Hero> Heroes { get; set; }
    }
}
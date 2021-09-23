using System;
using angular_heroes.Models;
using Microsoft.EntityFrameworkCore;

namespace angular_heroes.Data
{
    public class HeroesDbContext : DbContext
    {
        public HeroesDbContext(DbContextOptions<HeroesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Hero>().HasData(new Hero { Id = 11, Name = "Dr Nice", CreatedBy = "chris", CreatedDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { Id = 12, Name = "Narco", CreatedBy = "chris", CreatedDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { Id = 13, Name = "Bombasto", CreatedBy = "chris", CreatedDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { Id = 14, Name = "Celeritas", CreatedBy = "chris", CreatedDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { Id = 15, Name = "Magneta", CreatedBy = "chris", CreatedDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { Id = 16, Name = "RubberMan", CreatedBy = "chris", CreatedDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { Id = 17, Name = "Dynama", CreatedBy = "chris", CreatedDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { Id = 18, Name = "Dr IQ", CreatedBy = "chris", CreatedDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { Id = 19, Name = "Magma", CreatedBy = "chris", CreatedDate = DateTime.Now });
            modelBuilder.Entity<Hero>().HasData(new Hero { Id = 20, Name = "Tornado", CreatedBy = "chris", CreatedDate = DateTime.Now });

            modelBuilder.Entity<LogMessage>().HasData(new LogMessage { Id = 5, Contents = "Test Log Message", CreatedBy = "chris", CreatedDate = DateTime.Now });

            modelBuilder.Entity<LogMessage>(b => {
                b.HasKey(x => x.Id);
                b.Property(x => x.Contents).IsRequired();
                b.HasOne(x => x.Owner)
                 .WithMany(x => x.Messages);
            });

            modelBuilder.Entity<User>(b => {
                b.HasKey(x => x.Id);
                b.Property(x => x.UserName).IsRequired();
                b.HasMany(x => x.Heroes)
                 .WithMany(x => x.Users)
                 .UsingEntity<HeroUser>(x => x.HasOne(y => y.Hero)
                                                .WithMany(y => y.HeroUsers), 
                                        x => x.HasOne(y => y.User)
                                                .WithMany(y => y.HeroUsers));
            });
        }      

        public DbSet<Hero> Heroes { get; set; }
        public DbSet<LogMessage> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<HeroUser> HeroUsers { get; set; }
    }
}
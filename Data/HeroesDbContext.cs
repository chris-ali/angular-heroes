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
            var users = new User[] 
            {
                new User { Id = 1, UserName = "chris", FirstName = "Chris", LastName = "Ali", Email = "chris@ali.com"},
                new User { Id = 2, UserName = "somebodyElse", FirstName = "Somebody", LastName = "Else", Email = "somebody@else.com"},
            };
            modelBuilder.Entity<User>().HasData(users);

            var heroes = new Hero[] 
            {
                new Hero { Id = 11, Name = "Dr Nice", CreatedBy = "chris", CreatedDate = DateTime.Now },
                new Hero { Id = 12, Name = "Narco", CreatedBy = "chris", CreatedDate = DateTime.Now },
                new Hero { Id = 13, Name = "Bombasto", CreatedBy = "chris", CreatedDate = DateTime.Now },
                new Hero { Id = 14, Name = "Celeritas", CreatedBy = "chris", CreatedDate = DateTime.Now },
                new Hero { Id = 15, Name = "Magneta", CreatedBy = "chris", CreatedDate = DateTime.Now },
                new Hero { Id = 16, Name = "RubberMan", CreatedBy = "chris", CreatedDate = DateTime.Now },
                new Hero { Id = 17, Name = "Dynama", CreatedBy = "chris", CreatedDate = DateTime.Now },
                new Hero { Id = 18, Name = "Dr IQ", CreatedBy = "chris", CreatedDate = DateTime.Now },
                new Hero { Id = 19, Name = "Magma", CreatedBy = "chris", CreatedDate = DateTime.Now },
                new Hero { Id = 20, Name = "Tornado", CreatedBy = "chris", CreatedDate = DateTime.Now },
            };
            modelBuilder.Entity<Hero>().HasData(heroes);

            var messages = new LogMessage[] 
            {
                new LogMessage { Id = 5, Contents = "Test Log Message", CreatedBy = "chris", CreatedDate = DateTime.Now, UserIdFk = 1 },
                new LogMessage { Id = 6, Contents = "Another Test Log Message", CreatedBy = "chris", CreatedDate = DateTime.Now.AddDays(-1), UserIdFk = 1 },
                new LogMessage { Id = 7, Contents = "Someone Else's Test Log Message", CreatedBy = "somebodyElse", CreatedDate = DateTime.Now.AddMonths(-1), UserIdFk = 2 }
            };
            modelBuilder.Entity<LogMessage>().HasData(messages);

            var heroUsers = new HeroUser[] 
            {
                new HeroUser{ HeroIdFk = 11, UserIdFk = 1 },
                new HeroUser{ HeroIdFk = 13, UserIdFk = 2 },
                new HeroUser{ HeroIdFk = 11, UserIdFk = 2 },
                new HeroUser{ HeroIdFk = 15, UserIdFk = 1 },
                new HeroUser{ HeroIdFk = 12, UserIdFk = 1 },
            };
            modelBuilder.Entity<HeroUser>().HasData(heroUsers);

            modelBuilder.Entity<LogMessage>(b => 
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Contents).IsRequired();
                b.HasOne(x => x.Owner)
                 .WithMany(x => x.Messages)
                 .HasForeignKey(x => x.UserIdFk);
            });

            modelBuilder.Entity<User>(b => 
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.UserName).IsRequired();
                b.HasMany(x => x.Heroes)
                 .WithMany(x => x.Users)
                 .UsingEntity<HeroUser>(x => x.HasOne(y => y.Hero)
                                              .WithMany()
                                              .HasForeignKey(x => x.HeroIdFk), 
                                        x => x.HasOne(y => y.User)
                                              .WithMany()
                                              .HasForeignKey(x => x.UserIdFk))
                 .ToTable("HeroUsers").HasKey(x => new { x.HeroIdFk, x.UserIdFk});
            });
        }      

        public DbSet<Hero> Heroes { get; set; }
        public DbSet<LogMessage> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<HeroUser> HeroUsers { get; set; }
    }
}
using System;
using angular_heroes.Models;
using Microsoft.EntityFrameworkCore;

namespace angular_heroes.Data
{
    public class LogMessageContext : DbContext
    {
        public LogMessageContext(DbContextOptions<LogMessageContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<LogMessage>().HasData(new LogMessage { id = 5, contents = "Test Log Message", createdBy = "chris", createdDate = DateTime.Now });
        }

        public DbSet<LogMessage> Messages { get; set; }
    }
}
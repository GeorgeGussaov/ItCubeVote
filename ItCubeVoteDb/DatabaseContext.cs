using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ItCubeVoteDb.Models;

namespace ItCubeVoteDb
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<User> Users { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasData(new List<Project>()
            {
                new Project(){Id = Guid.NewGuid(), Name = "First"},
                new Project(){Id = Guid.NewGuid(), Name = "Second"},
				new Project() { Id = Guid.NewGuid(), Name = "Third" }, 
                new Project() { Id = Guid.NewGuid(), Name = "Fourth" },
				new Project() { Id = Guid.NewGuid(), Name = "Fifth" }
			});

            modelBuilder.Entity<User>().HasData(new List<User>() 
            {
                new User() {Id = Guid.NewGuid(), Login="Admin", Password="Admin"}
            });

		}
    }
}

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

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasData(new List<Project>()
            {
                new Project("First"){Id = Guid.NewGuid()}, new Project("Second"){Id = Guid.NewGuid()},
				new Project("Third"){Id = Guid.NewGuid()}, new Project("Fourth"){Id = Guid.NewGuid()},
				new Project("Fifth"){Id = Guid.NewGuid()}
			});
        }
    }
}

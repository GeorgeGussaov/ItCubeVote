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
                new Project(){Id = Guid.NewGuid(), Name = "First", Description="Описание проекта", FirsAuthor="Первый автор", SecondAuthor="Второй автор"},
                new Project(){Id = Guid.NewGuid(), Name = "Second", Description="Описание проекта", FirsAuthor="Первый автор", SecondAuthor="Второй автор"},
				new Project() { Id = Guid.NewGuid(), Name = "Third", Description="Описание проекта", FirsAuthor="Первый автор", SecondAuthor="Второй автор" }, 
                new Project() { Id = Guid.NewGuid(), Name = "Fourth", Description="Описание проекта", FirsAuthor="Первый автор", SecondAuthor="Второй автор" },
				new Project() { Id = Guid.NewGuid(), Name = "Fifth", Description="Описание проекта", FirsAuthor="Первый автор", SecondAuthor="Второй автор" }
			});

            modelBuilder.Entity<User>().HasData(new List<User>() 
            {
                new User() {Id = Guid.NewGuid(), Login="Admin", Password="Admin"}
            });

		}
    }
}

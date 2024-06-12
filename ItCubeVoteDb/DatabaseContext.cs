using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ItCubeVoteDb.Models;
using System.Linq;

namespace ItCubeVoteDb
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Project>().HasData(new List<Project>()
            //{
            //    new Project(){Id = Guid.NewGuid(), Name = "First", Description="Описание проекта", FirsAuthor="Первый автор", SecondAuthor="Второй автор"},
            //    new Project(){Id = Guid.NewGuid(), Name = "Second", Description="Описание проекта", FirsAuthor="Первый автор", SecondAuthor="Второй автор"},
            //    new Project() { Id = Guid.NewGuid(), Name = "Third", Description="Описание проекта", FirsAuthor="Первый автор", SecondAuthor="Второй автор" },
            //    new Project() { Id = Guid.NewGuid(), Name = "Fourth", Description="Описание проекта", FirsAuthor="Первый автор", SecondAuthor="Второй автор" },
            //    new Project() { Id = Guid.NewGuid(), Name = "Fifth", Description="Описание проекта", FirsAuthor="Первый автор", SecondAuthor="Второй автор" }
            //});

            
            //modelBuilder.Entity<Date>().HasData(new List<Date>() 
            //{
            //    new Date(){ Id = Guid.NewGuid(), DateTime = DateTime.Now, Projects = new List<Project>() {
            //        new Project(){Id = Guid.NewGuid(), Name = "First", Description="Описание проекта", FirsAuthor="Первый автор", SecondAuthor="Второй автор"},
            //        new Project(){Id = Guid.NewGuid(), Name = "Second", Description="Описание проекта", FirsAuthor="Первый автор", SecondAuthor="Второй автор"},
            //        new Project() { Id = Guid.NewGuid(), Name = "Third", Description="Описание проекта", FirsAuthor="Первый автор", SecondAuthor="Второй автор" },
            //        new Project() { Id = Guid.NewGuid(), Name = "Fourth", Description="Описание проекта", FirsAuthor="Первый автор", SecondAuthor="Второй автор" },
            //        new Project() { Id = Guid.NewGuid(), Name = "Fifth", Description="Описание проекта", FirsAuthor="Первый автор", SecondAuthor="Второй автор" }
            //        } 
            //    }
            //});

        }
    }
}

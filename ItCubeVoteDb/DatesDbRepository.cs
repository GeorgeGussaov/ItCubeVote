using ItCubeVoteDb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItCubeVoteDb
{
    public class DatesDbRepository : IDates
    {
        private DatabaseContext _dbContext;
        public DatesDbRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Date date)
        {
            _dbContext.Dates.Add(date);
            _dbContext.SaveChanges();
        }
        public void AddVote(Vote vote)
        {
            _dbContext.Dates.OrderBy(x => x.DateTime).Include(x => x.Votes).Include(x => x.Projects).LastOrDefault().Votes.Add(vote);   //голос добавляется в последнюю дату, добавленную админом.
            _dbContext.SaveChanges();
        }
        public void AddProject(Project project)
        {
            if (_dbContext.Dates.Count() != 0)      //Если будет попытка создать проект, когда нет никакой даты, то вместо ошибки проект просто не сохранится :)
            {
                _dbContext.Dates.OrderBy(x => x.DateTime).Include(x => x.Projects).Include(x => x.Votes).LastOrDefault().Projects.Add(project);   //то же самое с проектами
                _dbContext.SaveChanges();
            }
        }
        public List<Date> GetDates()
        {
            return _dbContext.Dates.ToList();
        }
        public Date GetCurrentDate()
        {
            if(_dbContext.Dates.Count() != 0)
            {
                return _dbContext.Dates.Include(x => x.Projects).Include(x => x.Votes).ThenInclude(x => x.User).OrderBy(x => x.DateTime).LastOrDefault();
            }
            return null;
        }
        public Date TryGetDateById(Guid id)
        {
            return _dbContext.Dates.Include(x => x.Projects).Include(x => x.Votes).Include(x => x.Votes).ThenInclude(x => x.User).FirstOrDefault(x => x.Id == id);
        }


        public List<Project> TryGetProjectsById(Guid id)
        {
            return _dbContext.Dates.Include(x => x.Projects).Include(x => x.Votes).FirstOrDefault(d => d.Id == id).Projects;
        }
        public void EditProject(Guid id, Project project)
        {
            foreach (var pr in _dbContext.Dates.FirstOrDefault(x => x.Id == id).Projects)
            {
                if (pr.Id == project.Id)
                {
                    pr.Name = project.Name;
                    pr.Description = project.Description;
                    pr.FirsAuthor = project.FirsAuthor;
                    pr.SecondAuthor = project.SecondAuthor;
                }
            }
            _dbContext.SaveChanges();
        }
    }

    public interface IDates
    {
        public void Add(Date date);
        public List<Date> GetDates();
        public void AddVote(Vote vote);
        public void AddProject(Project project);
        public Date GetCurrentDate();
        public Date TryGetDateById(Guid id);
        public List<Project> TryGetProjectsById(Guid id);
        public void EditProject(Guid id, Project project);
    }
}

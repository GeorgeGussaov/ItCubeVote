using ItCubeVoteDb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItCubeVoteDb
{
    public class EventsDbRepository : IEvents
    {
        private DatabaseContext _dbContext;
        public EventsDbRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Event date)
        {
            _dbContext.Events.Add(date);
            _dbContext.SaveChanges();
        }
        public void AddVote(Vote vote)
        {
            _dbContext.Events.OrderBy(x => x.DateTime).Include(x => x.Votes).Include(x => x.Projects).LastOrDefault().Votes.Add(vote);  //голос добавляется в последнюю дату, добавленную админом.
            _dbContext.SaveChanges();
        }
        public void AddProject(Project project)
        {
            if (_dbContext.Events.Count() != 0)      //Если будет попытка создать проект, когда нет никакой даты, то вместо ошибки проект просто не сохранится :)
            {
                _dbContext.Events.OrderBy(x => x.DateTime).Include(x => x.Projects).Include(x => x.Votes).LastOrDefault().Projects.Add(project);   //проект также добавляется в последнюю дату
                _dbContext.SaveChanges();
            }
        }
        public List<Event> GetDates()
        {
            return _dbContext.Events.ToList();
        }
        public Event GetCurrentDate()
        {
            if(_dbContext.Events.Count() != 0)
            {
                return _dbContext.Events.Include(x => x.Projects).Include(x => x.Votes).ThenInclude(x => x.User).OrderBy(x => x.DateTime).LastOrDefault();
            }
            return null;
        }
        public Event TryGetDateById(Guid id)
        {
            return _dbContext.Events.Include(x => x.Projects).Include(x => x.Votes).Include(x => x.Votes).ThenInclude(x => x.User).FirstOrDefault(x => x.Id == id);
        }


        public List<Project> TryGetProjectsById(Guid id)
        {
            return _dbContext.Events.Include(x => x.Projects).Include(x => x.Votes).FirstOrDefault(d => d.Id == id).Projects;
        }
        public void EditProject(Guid id, Project project)
        {
            foreach (var pr in _dbContext.Events.FirstOrDefault(x => x.Id == id).Projects)
            {
                if (pr.Id == project.Id)
                {
                    pr.Name = project.Name;
                    pr.Description = project.Description;
                    pr.FirsAuthor = project.FirsAuthor;
                    pr.SecondAuthor = project.SecondAuthor;
                    pr.GitLink = project.GitLink;
                    pr.ImgPath = project.ImgPath;
                }
            }
            _dbContext.SaveChanges();
        }

        public void DeleteDateById(Guid id)
        {
            _dbContext.Events.Remove(TryGetDateById(id));
            _dbContext.SaveChanges();
        }
    }

    public interface IEvents
    {
        public void Add(Event date);
        public List<Event> GetDates();
        public void AddVote(Vote vote);
        public void AddProject(Project project);
        public Event GetCurrentDate();
        public Event TryGetDateById(Guid id);
        public List<Project> TryGetProjectsById(Guid id);
        public void EditProject(Guid id, Project project);
        public void DeleteDateById(Guid id);

	}
}

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
            _dbContext.Dates.OrderBy(x => x.DateTime).Include(x => x.Projects).Include(x => x.Votes).LastOrDefault().Projects.Add(project);   //то же самое с проектами
            _dbContext.SaveChanges();
        }
        public List<Date> GetDates()
        {
            return _dbContext.Dates.ToList();
        }
        public Date GetCurrentDate()
        {
            return _dbContext.Dates.Include(x => x.Projects).Include(x => x.Votes).OrderBy(x => x.DateTime).LastOrDefault();
        }
        public Date TryGetDateById(Guid id)
        {
            return _dbContext.Dates.Include(x => x.Projects).Include(x => x.Votes).FirstOrDefault(x => x.Id == id);
        }

        public List<Project> TryGetProjectsById(Guid id)
        {
            return _dbContext.Dates.Include(x => x.Projects).Include(x => x.Votes).FirstOrDefault(d => d.Id == id).Projects;
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
    }
}

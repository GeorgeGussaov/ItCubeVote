using ItCubeVoteDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItCubeVoteDb
{
	public class ProjectsDbRepository : IProjects
	{
		private DatabaseContext _dbContext;
		public ProjectsDbRepository(DatabaseContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void Add(Project project)
		{
			_dbContext.Projects.Add(project);
			_dbContext.SaveChanges();
		}

		public List<Project> GetProjects()
		{
			return _dbContext.Projects.ToList();
		}

		public Project TryGetProjectById(Guid id)
		{
			return _dbContext.Projects.FirstOrDefault(x => x.Id == id);

		}
	}

	public interface IProjects
	{
		public void Add(Project project);
		public List<Project> GetProjects();
		public Project TryGetProjectById(Guid id);
	}
}

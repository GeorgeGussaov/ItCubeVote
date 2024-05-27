using ItCubeVoteDb.Models;
using System;
using System.Collections.Generic;
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
		}
	}

	public interface IProjects
	{
		public void Add(Project project);
	}
}

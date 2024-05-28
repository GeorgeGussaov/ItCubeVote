using ItCubeVote.Models;
using ItCubeVoteDb.Models;
using System.Collections.Generic;

namespace ItCubeVote.Helpers
{
	public static class Mapping
	{
		public static List<ProjectViewModel> ToProjectsViewsModel(List<Project> projects)
		{
			var projectsView = new List<ProjectViewModel>();
			foreach (var project in projects)
			{
				projectsView.Add(new ProjectViewModel() { Name = project.Name, Id=project.Id,
					Description = project.Description,
					FirsAuthor = project.FirsAuthor,
					SecondAuthor = project.SecondAuthor
				});
			}
			return projectsView;
		}

		public static Project ToProject(ProjectViewModel projectView)
		{
			return new Project() { Id = projectView.Id, Name = projectView.Name, Description = projectView.Description,
				FirsAuthor=projectView.FirsAuthor, SecondAuthor= projectView.SecondAuthor };
		}
		public static ProjectViewModel ToProjectViewModel(Project project)
		{
			return new ProjectViewModel() { Id = project.Id, Name = project.Name, Description = project.Description,
				FirsAuthor = project.FirsAuthor, SecondAuthor = project.SecondAuthor };
		}
	}
}

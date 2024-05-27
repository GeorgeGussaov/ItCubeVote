using ItCubeVote.Models;
using ItCubeVoteDb.Models;
using System.Collections.Generic;

namespace ItCubeVote.Helpers
{
	public static class Mapping
	{
		public static List<ProjectViewModel> ToProjectViewsModel(List<Project> projects)
		{
			var projectsView = new List<ProjectViewModel>();
			foreach (var project in projects)
			{
				projectsView.Add(new ProjectViewModel() { Name = project.Name, Id=project.Id });
			}
			return projectsView;
		}
	}
}

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



		public static List<VoteViewModel> ToVotesViewModel(List<Vote> votes)
		{
			var result = new List<VoteViewModel>();
			foreach(var vote in votes)
			{
				result.Add(new VoteViewModel() 
				{
					Id=vote.Id,
					User = vote.User,
					MostBeautiful = vote.MostBeautiful,
					MostDificult = vote.MostDificult,
					Coolest = vote.Coolest
				});
			}
			return result;
		}





		public static User ToUser(UserViewModel user)
		{
			return new User() 
			{
				Name = user.Name,
				School = user.School,
				Class = user.Class
			};
		}
	}
}

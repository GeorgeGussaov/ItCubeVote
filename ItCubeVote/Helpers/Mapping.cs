using ItCubeVote.Models;
using ItCubeVoteDb.Models;
using System.Collections.Generic;

namespace ItCubeVote.Helpers
{
	public static class Mapping
	{
		public static List<ProjectViewModel> ToProjectsViewsModel(List<Project> projects)
		{
			if (projects == null || projects.Count == 0) return null;
			var projectsView = new List<ProjectViewModel>();
			foreach (var project in projects)
			{
				projectsView.Add(new ProjectViewModel() 
				{ 
					Name = project.Name, 
					Id=project.Id,
					Description = project.Description,
					FirsAuthor = project.FirsAuthor,
					SecondAuthor = project.SecondAuthor
				});
			}
			return projectsView;
		}

		public static Project ToProject(ProjectViewModel projectView)
		{
			return new Project() 
			{ 
				Id = projectView.Id,
				Name = projectView.Name,
				Description = projectView.Description,
				FirsAuthor=projectView.FirsAuthor, 
				SecondAuthor= projectView.SecondAuthor 
			};
		}
		public static ProjectViewModel ToProjectViewModel(Project project)
		{
			return new ProjectViewModel() 
			{ 
				Id = project.Id, 
				Name = project.Name, 
				Description = project.Description,
				FirsAuthor = project.FirsAuthor,
				SecondAuthor = project.SecondAuthor,
				GitLink = project.GitLink,
				ImgPath = project.ImgPath
			};
		}
		public static List<Project> ToProjects(List<ProjectViewModel> projectsView)
		{
			var result = new List<Project>();
			foreach(var  project in projectsView)
			{
				result.Add(new Project()
				{
					Id = project.Id,
					Name = project.Name,
					Description = project.Description,
					FirsAuthor = project.FirsAuthor,
					SecondAuthor = project.SecondAuthor,
					GitLink = project.GitLink,
					ImgPath = project.ImgPath
				});
			}
			return result;
		}
		public static List<ProjectViewModel> ToProjectsViewModel(List<Project> projects)
		{
			var result = new List<ProjectViewModel>();
			foreach(var project in projects)
			{
				result.Add(new ProjectViewModel()
				{
					Id = project.Id,
					Name = project.Name,
					Description = project.Description,
					FirsAuthor = project.FirsAuthor,
					SecondAuthor = project.SecondAuthor,
					GitLink = project.GitLink,
					ImgPath = project.ImgPath
				});
			}
			return result;
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
					MostBeautiful = ToProjectViewModel(vote.MostBeautiful),
					MostDificult = ToProjectViewModel(vote.MostDificult),
					Coolest = ToProjectViewModel(vote.Coolest),
					Time = vote.Time
				});
			}
			return result;
		}
        public static List<Vote> ToVotes(List<VoteViewModel> votesView)
        {
            var result = new List<Vote>();
            foreach (var vote in votesView)
            {
                result.Add(new Vote()
                {
                    Id = vote.Id,
                    User = vote.User,
                    MostBeautiful = ToProject(vote.MostBeautiful),
                    MostDificult = ToProject(vote.MostDificult),
                    Coolest = ToProject(vote.Coolest),
					Time = vote.Time
                });
            }
            return result;
        }




        public static User ToUser(UserViewModel user)
		{
			return new User() 
			{
				Id = user.Id,
				Name = user.Name,
				School = user.School,
				Class = user.Class
			};
		}
		public static UserViewModel ToUserViewModel(User user)
		{
			return new UserViewModel()
			{
				Id = user.Id,
				Name = user.Name,
				School = user.School,
				Class = user.Class
			};
		}


		


		public static Date ToDate(DateViewModel date)
		{
			return new Date() 
			{
				Id=date.Id,
				DateTime = date.DateTime,
				//Votes = ToVotes(date.Votes),
				//Projects = ToProjects(date.Projects)
			};
		}
		public static List<DateViewModel> ToDatesViewModel(List<Date> dates)
		{
			var result = new List<DateViewModel>();
			foreach(var date in dates)
			{
				result.Add(new DateViewModel()
				{
					Id = date.Id,
					DateTime = date.DateTime,
					//Votes = ToVotesViewModel(date.Votes),
					//Projects = ToProjectsViewModel(date.Projects)
                });
			}
			return result;
		}
	}
}

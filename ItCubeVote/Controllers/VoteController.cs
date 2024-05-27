using ItCubeVote.Helpers;
using ItCubeVoteDb;
using ItCubeVoteDb.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ItCubeVote.Controllers
{
	public class VoteController : Controller
	{
		private readonly IProjects projectsDb;
		private readonly IVotes votesDb;
		public VoteController(IProjects projects, IVotes votes)
		{
			this.projectsDb = projects;
			this.votesDb = votes;
		}
		public IActionResult Index()
		{
			var projects = projectsDb.GetProjects();
			return View(Mapping.ToProjectsViewsModel(projects));
		}
		public IActionResult ToConfirm(Guid MostDificult, Guid MostBeautiful, Guid Coolest)
		{
			Vote newVote = new Vote()
			{
				MostDificult = projectsDb.TryGetProjectById(MostDificult),
				MostBeautiful = projectsDb.TryGetProjectById(MostBeautiful),
				Coolest = projectsDb.TryGetProjectById(Coolest)
			};
			votesDb.Add(newVote);
			return RedirectToAction("Index");
		}
	}
}

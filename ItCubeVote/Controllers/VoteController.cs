using ItCubeVote.Helpers;
using ItCubeVoteDb;
using ItCubeVoteDb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ItCubeVote.Controllers
{
	public class VoteController : Controller
	{
		private readonly IProjects projectsDb;
		private readonly IVotes votesDb;
		private readonly IUsers usersDb;
		public VoteController(IProjects projects, IVotes votes, IUsers usersDb)
		{
			this.projectsDb = projects;
			this.votesDb = votes;
			this.usersDb = usersDb;
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
				Coolest = projectsDb.TryGetProjectById(Coolest),
				User = usersDb.GetUsers().Last() //Очень грубый костыль. Берет последнего пользователя из бд, подразумевая что он текущий. Когда подключу куки буду брать оттуда.
			};
			votesDb.Add(newVote);
			return RedirectToAction("Index");
		}
	}
}

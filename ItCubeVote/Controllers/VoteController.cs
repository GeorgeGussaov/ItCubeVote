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
		private readonly IEvents datesDb;
		public VoteController(IProjects projects, IVotes votes, IUsers users, IEvents dates)
		{
			projectsDb = projects;
			votesDb = votes;
			usersDb = users;
			datesDb = dates;
		}
		public IActionResult Index()
		{
			var votes = datesDb?.GetCurrentDate()?.Votes ?? null;
			if( votes != null)
			{
				foreach (var vote in votes)
				{
					if (vote.User.Id == Guid.Parse(Request.Cookies["user"])) return RedirectToAction("Thanks");        //Если пользователь уже голосвал в этом сезоне,
				}
			}                                                                                                    //то его сразу перекинет на страницу благодарности

			var projects = datesDb?.GetCurrentDate()?.Projects ?? null;
            ViewBag.CountVotes = datesDb?.GetCurrentDate()?.Votes.Count ?? 0;
            return View(Mapping.ToProjectsViewsModel(projects));
		}
		public IActionResult Thanks()
		{
			return View();
		}
		public IActionResult Warning()
		{
			return View();
		}
		public IActionResult ToConfirm(Guid MostDificult, Guid MostBeautiful, Guid Coolest)
		{
			var votes = datesDb?.GetCurrentDate()?.Votes ?? null;
			if(votes != null)
			{
				foreach (var vote in votes)
				{
					if (vote.User.Id == Guid.Parse(Request.Cookies["user"])) return RedirectToAction("Warning");        //Если пользователь уже голосвал в этом сезоне и пытается
				}
			}                                                                                                     //проголосовать еще раз, то его перекинет на страницу предупреждения
			Vote newVote = new Vote()
			{
				MostDificult = projectsDb.TryGetProjectById(MostDificult),
				MostBeautiful = projectsDb.TryGetProjectById(MostBeautiful),
				Coolest = projectsDb.TryGetProjectById(Coolest),
				//User = usersDb.GetUsers().Last(), //Очень грубый костыль. Берет последнего пользователя из бд, подразумевая что он текущий. Когда подключу куки буду брать оттуда.
				User = usersDb.TryGetUserById(Guid.Parse(Request.Cookies["user"]))
			};
			datesDb.AddVote(newVote);
			return RedirectToAction("Thanks");
		}
	}
}

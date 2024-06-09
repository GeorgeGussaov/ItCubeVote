using ItCubeVote.Areas.Admin.Models;
using ItCubeVote.Helpers;
using ItCubeVote.Models;
using ItCubeVoteDb;
using ItCubeVoteDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace ItCubeVote.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MainController : Controller
	{
		private readonly IProjects projectsDb;
		private readonly IDates datesDb;
		private readonly IUsers usersDb;
		public MainController(IProjects projects, IDates dates, IUsers users)
		{
			projectsDb= projects;
			datesDb= dates;
			usersDb= users;
		}
		public IActionResult Login()
		{
			return View();
		}

		public IActionResult Index()
		{
			var dates = datesDb.GetDates().OrderBy(x => x.DateTime).Reverse().ToList();
			return View(Mapping.ToDatesViewModel(dates));
		}

		public IActionResult NewProject()
		{
			return View();
		}


		public IActionResult NewDate()
		{
			return View();
		}



		public IActionResult Votes(Guid id)
		{
			var votes = datesDb.TryGetDateById(id).Votes;
			votes.Reverse(); //чтобы новые голоса были выше старых
			return View(Mapping.ToVotesViewModel(votes));
		}


		public IActionResult Results(Guid id)
		{
			var votes = Mapping.ToVotesViewModel(datesDb.TryGetDateById(id).Votes);
			var projects = Mapping.ToProjectsViewModel(datesDb.TryGetDateById(id).Projects);
			var projectResults = new List<ProjectResult>();
			foreach(var project in projects)
			{
				projectResults.Add(new ProjectResult() { Project = project });
			}

			foreach(var result in projectResults)
			{
				foreach(var vote in votes)
				{
					if (result.Project.Id == vote.MostBeautiful.Id) result.CntMostBeautifulVotes++;
					else if(result.Project.Id == vote.MostDificult.Id) result.CntMostDificultVotes++;
					else if(result.Project.Id == vote.Coolest.Id) result.CntCoolestVotes++;
				}
				result.CountAll();
			}

			return View(projectResults.OrderBy(x => x.CntAll).Reverse().ToList());
		}


		public IActionResult Delete(Guid id)
		{
			datesDb.DeleteDateById(id);
			return RedirectToAction("Index");
		}



		public IActionResult UserInfo(Guid id)
		{
			var user = usersDb.TryGetUserById(id);
			return View(Mapping.ToUserViewModel(user));
		}


		[HttpPost]
		public IActionResult Enter(AdminCheck admin)
		{
			if (ModelState.IsValid)
			{
				if (admin.Login != "Admin" && admin.Password != "Admin")
				{
					ModelState.AddModelError("", "Введен неправильный логин или пароль");
					return View("Login");
				}
				return RedirectToAction("Index");
			}
			return View("Login");
		}

		public IActionResult AddNewProject(ProjectViewModel project)
		{
            if (ModelState.IsValid)
            {
				if(datesDb != null) datesDb.AddProject(Mapping.ToProject(project));
				//projectsDb.Add(Mapping.ToProject(project));
				return RedirectToAction("Index");
			}
			return View("NewProject");
		}
        public IActionResult AddDate(DateViewModel date)
        {
			if (ModelState.IsValid)
			{
				var dates = Mapping.ToDatesViewModel(datesDb.GetDates());
				foreach(var item in dates)
				{
					if(item.DateTime > date.DateTime)	//ну по тексту ошибки понятно зачем этот форич
					{
						ModelState.AddModelError("", "Указана неактуальная дата(у предыдущего события дата свежее)");
						return View("NewDate");
					}
				}
				datesDb.Add(Mapping.ToDate(date));
				return RedirectToAction("Index");
			}
            return View("NewDate");
        }
    }
}

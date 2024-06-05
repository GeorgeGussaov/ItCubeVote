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
				datesDb.AddProject(Mapping.ToProject(project));
				//projectsDb.Add(Mapping.ToProject(project));
				return RedirectToAction("Index");
			}
			return View("NewProject");
		}
        public IActionResult AddDate(DateViewModel date)
        {
			if (ModelState.IsValid)
			{
				datesDb.Add(Mapping.ToDate(date));
				return RedirectToAction("Index");
			}
            return View("NewDate");
        }
    }
}

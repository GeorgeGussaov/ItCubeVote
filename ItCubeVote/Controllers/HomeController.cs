using ItCubeVote.Helpers;
using ItCubeVote.Models;
using ItCubeVoteDb;
using ItCubeVoteDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ItCubeVote.Controllers
{
	public class HomeController : Controller
	{
		private readonly IProjects projectsDb;
		private readonly IUsers usersDb;
		public HomeController(IProjects projects, IUsers users) 
		{
			this.projectsDb = projects;
			usersDb = users;
		}

		public IActionResult Index()
		{
			return View();
		}


		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpPost]
		public IActionResult Login(UserViewModel user)
		{
			if (ModelState.IsValid)
			{
				usersDb.Add(Mapping.ToUser(user));
				return RedirectToAction("Index", "Vote");
			}
			return View("Index");
		}
	}
}

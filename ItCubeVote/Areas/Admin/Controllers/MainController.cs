using ItCubeVote.Areas.Admin.Models;
using ItCubeVote.Helpers;
using ItCubeVote.Models;
using ItCubeVoteDb;
using ItCubeVoteDb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;

namespace ItCubeVote.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MainController : Controller
	{
		private readonly IProjects projectsDb;
		private readonly IEvents datesDb;
		private readonly IUsers usersDb;
		private readonly IWebHostEnvironment appEnvironment;
		public MainController(IProjects projects, IEvents dates, IUsers users, IWebHostEnvironment app)
		{
			projectsDb= projects;
			datesDb= dates;
			usersDb= users;
			appEnvironment= app;
		}
		public IActionResult Login()
		{
			var Cookie = Request.Cookies["admin"];
			if (Cookie != null) return RedirectToAction("Index");

			return View();
		}
		public IActionResult Warning()
		{
			return View();
		}

		public IActionResult Index()
		{
			var Cookie = Request.Cookies["admin"];
			if (Cookie == null) return RedirectToAction("Warning", "Main");

			var dates = datesDb.GetDates().OrderBy(x => x.DateTime).Reverse().ToList();
			return View(Mapping.ToEventsViewModel(dates));
		}

		public IActionResult NewProject()
		{
			return View();
		}


		public IActionResult NewDate()
		{
			var Cookie = Request.Cookies["admin"];
			if (Cookie == null) return RedirectToAction("Warning", "Main");

			return View();
		}



		public IActionResult Votes(Guid id)
		{
			var Cookie = Request.Cookies["admin"];
			if (Cookie == null) return RedirectToAction("Warning", "Main");

			var votes = datesDb.TryGetDateById(id).Votes;
			votes.Reverse(); //чтобы новые голоса были выше старых
			return View(Mapping.ToVotesViewModel(votes));
		}


		public IActionResult Results(Guid id)
		{
			var Cookie = Request.Cookies["admin"];
			if (Cookie == null) return RedirectToAction("Warning", "Main");		//результаты пока что только для админа

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
			var Cookie = Request.Cookies["admin"];
			if (Cookie == null) return RedirectToAction("Warning", "Main");

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
				CookieOptions cookie = new CookieOptions();
				cookie.Expires = DateTime.Now.AddHours(1);	//каждый час в админа надо перезаходить 
				Response.Cookies.Append("admin", "admin", cookie);
				return RedirectToAction("Index");
			}
			return View("Login");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddNewProject(ProjectViewModel project)
		{
            if (ModelState.IsValid)
			{

				var folderPath = Path.Combine(appEnvironment.WebRootPath + "/images/projects/");
				if (!Directory.Exists(folderPath))
				{
					Directory.CreateDirectory(folderPath);
				}

				var fileName = Guid.NewGuid() + "." + project.Img.FileName.Split('.').Last();
				//string path = Path.Combine(folderPath, fileName);
				using (var fileStream = new FileStream(folderPath + fileName, FileMode.Create))
				{
					project.Img.CopyTo(fileStream);
				}

				var projectDb = new Project()
				{
					Name = project.Name,
					FirsAuthor = project.FirsAuthor,
					SecondAuthor = project.SecondAuthor,
					Description = project.Description,
					GitLink = project.GitLink,
					ImgPath = "/images/projects/" + fileName
				};


				if (datesDb != null) datesDb.AddProject(projectDb);
				//projectsDb.Add(Mapping.ToProject(project));
				return RedirectToAction("Index");
			}
			return View("NewProject");
		}
        public IActionResult AddDate(EventViewModel date)
        {
			var Cookie = Request.Cookies["admin"];
			if (Cookie == null) return RedirectToAction("Warning", "Main");

			if (ModelState.IsValid)
			{
				var dates = Mapping.ToEventsViewModel(datesDb.GetDates());
				foreach(var item in dates)
				{
					if(item.DateTime > date.DateTime)	//ну по тексту ошибки понятно зачем этот форич
					{
						ModelState.AddModelError("", "Указана неактуальная дата(у предыдущего события дата свежее)");
						return View("NewDate");
					}
				}
				datesDb.Add(Mapping.ToEvent(date));
				return RedirectToAction("Index");
			}
            return View("NewDate");
        }
    }
}

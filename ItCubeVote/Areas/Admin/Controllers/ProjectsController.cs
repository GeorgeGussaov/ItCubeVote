using ItCubeVote.Helpers;
using ItCubeVote.Models;
using ItCubeVoteDb;
using ItCubeVoteDb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace ItCubeVote.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProjectsController : Controller
	{
		private readonly IProjects projectsDb;
		private readonly IDates datesDb;
		private readonly IWebHostEnvironment appEnvironment;
		public ProjectsController(IProjects projects, IDates dates, IWebHostEnvironment app)
		{
			projectsDb = projects;
			datesDb = dates;
			appEnvironment = app;
		}
		public IActionResult Index(Guid id)
		{
			var Cookie = Request.Cookies["admin"];
			if (Cookie == null) return RedirectToAction("Warning", "Main");

			var projects = Mapping.ToProjectsViewModel(datesDb.TryGetProjectsById(id));
			var votes = Mapping.ToVotesViewModel(datesDb.TryGetDateById(id).Votes);
			foreach(var vote in votes)
			{
				foreach(var project in projects)		//весь вложенный цикл только чтобы проверить на наличие проекта среди голосов и в случае отсутствия голоса за проект
				{										//дать админу возможность удалить этот проект(сделано только чтобы если проект добавлен случайно можно было снести)
					if (vote.MostDificult.Id == project.Id || vote.MostBeautiful.Id == project.Id || vote.Coolest.Id == project.Id) project.IsRemovable = false;
				}
			}
			return View(projects);
		}
		public IActionResult Project(Guid id)
		{
			var project = projectsDb.TryGetProjectById(id);
			return View(Mapping.ToProjectViewModel(project));
		}
		public IActionResult Edit(Guid Id)
		{
			var Cookie = Request.Cookies["admin"];
			if (Cookie == null) return RedirectToAction("Warning", "Main");

			var project = projectsDb.TryGetProjectById(Id);
			return View(Mapping.ToProjectViewModel(project));
		}
		public IActionResult Delete(Guid id)
		{
			var Cookie = Request.Cookies["admin"];
			if (Cookie == null) return RedirectToAction("Warning", "Main");

			projectsDb.DeleteProject(id);
			return RedirectToAction("Index", "Main");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditProject(ProjectViewModel project)
		{
			var Cookie = Request.Cookies["admin"];
			if (Cookie == null) return RedirectToAction("Warning", "Main");

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
					Id = project.Id,
					Name = project.Name,
					FirsAuthor = project.FirsAuthor,
					SecondAuthor = project.SecondAuthor,
					Description = project.Description,
					GitLink = project.GitLink,
					ImgPath = "/image/projects/" + fileName
				};

				var curDate = datesDb.GetCurrentDate().Id;
				datesDb.EditProject(curDate,projectDb);
				return RedirectToAction("Index", "Main");
			}
			return View("Edit");
		}
	}
}

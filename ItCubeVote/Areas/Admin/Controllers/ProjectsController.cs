using ItCubeVote.Helpers;
using ItCubeVote.Models;
using ItCubeVoteDb;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ItCubeVote.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProjectsController : Controller
	{
		private readonly IProjects projectsDb;
		public ProjectsController(IProjects projects)
		{
			projectsDb = projects;
		}
		public IActionResult Index()
		{
			var projects = projectsDb.GetProjects();
			return View(Mapping.ToProjectsViewsModel(projects));
		}
		public IActionResult Edit(Guid Id)
		{
			var project = projectsDb.TryGetProjectById(Id);

			return View(Mapping.ToProjectViewModel(project));
		}

		[HttpPost]
		public IActionResult EditProject(ProjectViewModel project)
		{
			if (ModelState.IsValid)
			{
				projectsDb.RenameProject(project.Id, project.Name);
				return RedirectToAction("Index");
			}
			return View("Edit");
		}
	}
}

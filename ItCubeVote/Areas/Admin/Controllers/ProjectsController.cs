using ItCubeVote.Helpers;
using ItCubeVote.Models;
using ItCubeVoteDb;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ItCubeVote.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProjectsController : Controller
	{
		private readonly IProjects projectsDb;
		private readonly IDates datesDb;
		public ProjectsController(IProjects projects, IDates dates)
		{
			projectsDb = projects;
			datesDb = dates;
		}
		public IActionResult Index(Guid id)
		{
			
			var projects = datesDb.TryGetProjectsById(id);
			return View(Mapping.ToProjectsViewsModel(projects));
		}
		public IActionResult Project(Guid id)
		{
			var project = projectsDb.TryGetProjectById(id);
			return View(Mapping.ToProjectViewModel(project));
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
				projectsDb.EditProject(Mapping.ToProject(project));
				return RedirectToAction("Index");
			}
			return View("Edit");
		}
	}
}

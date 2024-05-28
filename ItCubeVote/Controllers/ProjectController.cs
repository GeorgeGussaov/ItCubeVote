using ItCubeVote.Helpers;
using ItCubeVoteDb;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ItCubeVote.Controllers
{
	public class ProjectController : Controller
	{
		private readonly IProjects projectsDb;
		public ProjectController(IProjects projects)
		{
			projectsDb = projects;
		}
		public IActionResult Index(Guid id)
		{
			var project = projectsDb.TryGetProjectById(id);
			return View(Mapping.ToProjectViewModel(project));
		}
	}
}

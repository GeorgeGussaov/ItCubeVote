using ItCubeVote.Helpers;
using ItCubeVote.Models;
using ItCubeVoteDb;
using ItCubeVoteDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ItCubeVote.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MainController : Controller
	{
		private readonly IProjects projectsDb;
		public MainController(IProjects projects)
		{
			projectsDb= projects;
		}
		public IActionResult Login()
		{
			return View();
		}


		public IActionResult NewProject()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(UserViewModel user)
		{
			if (ModelState.IsValid)
			{
				if (user.Login != "Admin" && user.Password != "Admin")
				{
					ModelState.AddModelError("", "Введен неправильный логин или пароль");
					return View("Login");
				}
				return View();
			}
			return View("Login");
		}

		public IActionResult AddNewProject(ProjectViewModel project)
		{

			if (ModelState.IsValid)
			{
				projectsDb.Add(Mapping.ToProject(project));
				return View("Index");
			}
			return View("NewProject");
		}
	}
}

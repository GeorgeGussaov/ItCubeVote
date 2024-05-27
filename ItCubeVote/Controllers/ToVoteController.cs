using Microsoft.AspNetCore.Mvc;

namespace ItCubeVote.Controllers
{
	public class ToVoteController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

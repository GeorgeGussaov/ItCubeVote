﻿using ItCubeVote.Helpers;
using ItCubeVote.Models;
using ItCubeVoteDb;
using ItCubeVoteDb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ItCubeVote.Controllers
{
	public class HomeController : Controller
	{
		private readonly IProjects projectsDb;
		private readonly IUsers usersDb;
		private readonly IEvents datesDb;
		public HomeController(IProjects projects, IUsers users, IEvents dates) 
		{
			this.projectsDb = projects;
			usersDb = users;
			datesDb = dates;
		}

		public IActionResult Index()
		{
			if (datesDb.GetCurrentDate() != null)
			{
				//if(DateTime.Now.ToShortDateString() != datesDb.GetCurrentDate().DateTime.ToShortDateString()) return RedirectToAction("NoEvents");
				ViewBag.CurrentDate = datesDb.GetCurrentDate().DateTime.ToShortDateString();
				var Cookie = Request.Cookies["user"];
				if (Cookie == null) return View();
				return RedirectToAction("Index", "Vote");
			}
			return RedirectToAction("NoEvents");
		}

		public IActionResult NoEvents()
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
				user.Id =  Guid.NewGuid();
				usersDb.Add(Mapping.ToUser(user));

				CookieOptions cookie = new CookieOptions();
				cookie.Expires = DateTime.Now.AddYears(5);
				Response.Cookies.Append("user", user.Id.ToString(), cookie);

				return RedirectToAction("Index", "Vote");
			}
			return View("Index");
		}
	}
}

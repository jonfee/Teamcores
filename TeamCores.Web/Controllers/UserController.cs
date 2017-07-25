﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamCores.Data.Entity;
using TeamCores.Domain.Services;
using TeamCores.Misc.Filters;
using TeamCores.Models;
using TeamCores.Web.ViewModel.User;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamCores.Web.Controllers
{
	[UserAuthorization]
	public class UserController : Controller
	{
		// GET: /<controller>/
		public IActionResult Index()
		{
			return View();
		}
	}
}

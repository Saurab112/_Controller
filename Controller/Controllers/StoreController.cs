﻿using Microsoft.AspNetCore.Mvc;

namespace _Controller.Controllers
{
	public class StoreController : Controller
	{
		[Route("store/books/{id}")]
		public IActionResult Book()
		
		{
			int id = Convert.ToInt32(Request.RouteValues["id"]);
			return Content($"<h1>Book Store: {id}</h1>", "text/html");
		}
	}
}

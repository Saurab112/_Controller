using Microsoft.AspNetCore.Mvc;

namespace _Controller.Controllers
{
	public class StoreController : Controller
	{
		[Route("store/books")]
		public IActionResult Book()
		{
			return Content("<h1>Book Store</h1>", "text/html");
		}
	}
}

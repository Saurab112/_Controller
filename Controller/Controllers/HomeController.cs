using Microsoft.AspNetCore.Mvc;

namespace _Controller.Controllers
{
	//controller class inherits from controller
	public class HomeController : Controller
	{

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Submit(string name)
		{
			return RedirectToAction("Index");
		}
	}
}

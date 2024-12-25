using _Controller.Models;
using Microsoft.AspNetCore.Mvc;

namespace _Controller.Controllers
{
	//controller class inherits from controller
	public class HomeController : Controller
	{

		[HttpGet]
		public IActionResult Index()
		{
			//return new ContentResult() { Content = "Hello from Index", ContentType = "text/plain" };

			//return Content("Hello from Index", "text/plain");

			return Content("<h1>Welcome</h1> <h2>Hello from Index</h2>", "text/html");
		}
		//JsonResult
		public JsonResult Person()
		{
			Person person = new Person() { person_id = Guid.NewGuid(), first_name="saurab", last_name = "raj", age = 16 };
			return Json(person);
			

		}
		[Route("file-download1")]
		public VirtualFileResult FileDownload()
		{
			return File("/sample.pdf", "application/pdf");
		}
		[Route("file-download2")]
		public PhysicalFileResult FileDownload2()
		{
			return PhysicalFile(@"c:\aspnetcore\sample.pdf", "application/pdf");
		}
		[Route("file-download3")]
		public FileContentResult FileDownload3()
		{
			byte[] bytes = System.IO.File.ReadAllBytes(@"c:\aspnetcore\sample.pdf");
			return File(bytes, "application/pdf");
		}



		[HttpPost]
		public IActionResult Submit(string name)
		{
			return RedirectToAction("Index");
		}

		[Route("/hello/{id}")]
		public IActionResult Hello(int id)
		{
			return Content($"Displaying product id:{id}");
		}
		[Route("/hi")]
		public IActionResult RedirectExample()
		{
			// Redirect to "About" action in "Home" controller with route values
			return new RedirectResult("/hello/1");  //redirecting to Hello action method
		}

		public IActionResult About(int id)
		{
			ViewBag.Message = $"Redirected to About with ID: {id}";
			return View();
		}
    

		public IActionResult ThankYou()
		{
			return View();
		}

		[Route("book")]
		public IActionResult Index1()    //http://localhost:5176/book?bookid=10&isloggedin=true  -> opens sample.pdf
		{
			//Book id should be applied
			if (!Request.Query.ContainsKey("bookid"))
			{
				Response.StatusCode = 400;
				return Content("Book id is not supplied");
			}

			//Book id can't be empty
			if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
			{
				Response.StatusCode = 400;
				return Content("Book id can't be null or empty");
			}

			//Book id should be between 1 to 1000
			int bookId = Convert.ToInt16(ControllerContext.HttpContext.Request.Query["bookid"]);
			if (bookId <= 0)
			{
				Response.StatusCode = 400;
				return Content("Book id can't be less than or equal to zero");
			}
			if (bookId > 1000)
			{
				Response.StatusCode = 400;
				return Content("Book id can't be greater than 1000");
			}

			//isloggedin should be true
			if (Convert.ToBoolean(Request.Query["isloggedin"]) == false)
			{
				//return Unauthorized("User must be authenticated");
				return StatusCode(401);
			}

			return File("/sample.pdf", "application/pdf");
		}
	}
}

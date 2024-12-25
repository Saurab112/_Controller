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
			Person person = new Person() { person_id = Guid.NewGuid(), first_name = "saurab", last_name = "raj", age = 16 };
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


		[Route("book")]
		public IActionResult Index1()    //http://localhost:5176/book?bookId=10&isloggedin=true  -> opens sample.pdf
		{
			//Book id should be applied
			if (!Request.Query.ContainsKey("bookId"))
			{
				Response.StatusCode = 400;
				return Content("Book id is not supplied");
			}

			//Book id can't be empty
			if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookId"])))
			{
				Response.StatusCode = 400;
				return Content("Book id can't be null or empty");
			}

			//Book id should be between 1 to 1000
			int bookId = Convert.ToInt16(ControllerContext.HttpContext.Request.Query["bookId"]);
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

			//return File("/sample.pdf", "application/pdf");
			//return new RedirectToActionResult("Books", "Store", new { id = bookId }); //302 - Found
			//return new RedirectToActionResult("Book", "Store", new { id = bookId }, permanent: true); //301 - Moved permanently

			//302 - Found - LocalRedirectResult
			//return new LocalRedirectResult($"store/books/{id}"); //302 - Found
			//return LocalRedirect($"store/books/{id}"); //302 - Found

			//301 - Moved Permanently - LocalRedirectResult
			return new LocalRedirectResult($"store/books/{bookId}", true); //301 - Moved Permanently
																		   //return LocalRedirectPermanent($"store/books/{id}"); //301 - Moved Permanently

			//return Redirect($"store/books/{id}"); //302 - Found
			//return RedirectPermanent($"store/books/{bookId}"); //301 - Moved Permanently
		}
	}
}



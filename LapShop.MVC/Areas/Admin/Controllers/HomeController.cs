using Microsoft.AspNetCore.Mvc;

namespace LapShop.MVC.Area.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
   
        public IActionResult Index()
        {
            return View();
        }

		public IActionResult Error()
		{
			return View();
		}

	}
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.MVC.Controllers
{
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

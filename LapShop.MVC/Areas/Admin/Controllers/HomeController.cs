namespace LapShop.MVC.Area.Admin.Controllers
{
	[Area(SharedData.AdminArea)]
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

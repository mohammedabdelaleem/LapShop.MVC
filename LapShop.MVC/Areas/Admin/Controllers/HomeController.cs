namespace LapShop.MVC.Area.Admin.Controllers
{

	[Area(DefaultRoles.Admin)]
	[Authorize(Roles = DefaultRoles.Admin)]
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

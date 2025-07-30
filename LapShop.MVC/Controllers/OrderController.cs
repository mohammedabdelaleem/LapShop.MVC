using Microsoft.AspNetCore.Mvc;

namespace LapShop.MVC.Controllers;
public class OrderController : Controller
{
	public IActionResult Cart()
	{
		return View();
	}
}

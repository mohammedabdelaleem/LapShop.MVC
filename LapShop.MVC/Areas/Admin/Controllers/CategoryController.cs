using LapShop.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.MVC.Areas.Admin.Controllers;
[Area("admin")]
public class CategoryController(ICategoryService categoryService) : Controller
{
	private readonly ICategoryService _categoryService = categoryService;

	public async Task<IActionResult> List(CancellationToken cancellationToken=default)
	{
		var categories = await _categoryService.GetAll(cancellationToken);
		return View(nameof(List) , categories);
	}
}

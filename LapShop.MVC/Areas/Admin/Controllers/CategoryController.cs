using LapShop.MVC.Models;
using LapShop.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.MVC.Areas.Admin.Controllers;
[Area("admin")]
public class CategoryController(ICategoryService categoryService) : Controller
{
	private readonly ICategoryService _categoryService = categoryService;

	[HttpGet]
	public async Task<IActionResult> List(CancellationToken cancellationToken=default)
	{
		var categories = await _categoryService.GetAllAsync(cancellationToken);
		return View(nameof(List) , categories);
	}

	[HttpGet]
	public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken=default)
	{
		if (id == null)
		{
			//add new  
			return View(nameof(Edit), new TbCategory());
		}
		
	
		var category = await _categoryService.GetAsync(id!.Value, cancellationToken);
		
			//edit 
			return View(nameof(Edit), category);
		
	
			

	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Save(TbCategory category, CancellationToken cancellationToken = default)
	{
		if (!ModelState.IsValid)
		{
			return View(nameof(Edit), category);
		}

		if (category.CategoryId == 0)
		{ 
			await _categoryService.AddAsync(category, cancellationToken); 
		}

		else
		{
			await _categoryService.UpdateAsync( category.CategoryId, category, cancellationToken);
		}


		var categories = await _categoryService.GetAllAsync(cancellationToken);
		return RedirectToAction(nameof(List), categories);	
	}

}

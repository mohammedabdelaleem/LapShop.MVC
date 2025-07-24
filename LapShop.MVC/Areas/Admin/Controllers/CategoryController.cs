namespace LapShop.MVC.Areas.Admin.Controllers;

[Area(SharedData.AdminArea)]
public class CategoryController(
	ICategoryService categoryService,
	IFileService fileService) : Controller
{
	private readonly ICategoryService _categoryService = categoryService;
	private readonly IFileService _fileService = fileService;

	[HttpGet]
	public async Task<IActionResult> List(CancellationToken cancellationToken = default)
	{
		var categories = await _categoryService.GetAllAsync(cancellationToken);
		return View(nameof(List), categories);
	}

	[HttpGet]
	public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken = default)
	{
		if (id == null)
		{
			//add new  
			return View(nameof(Edit), new TbCategory());
		}
		else
		{
			//edit 
			var category = await _categoryService.GetAsync(id!.Value, cancellationToken);
			return View(nameof(Edit), category);
		}

	}

	public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
	{
		var result = await _categoryService.DeleteAsync(id, cancellationToken);
		return RedirectToAction(nameof(List));
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Save(TbCategory category, IFormFile? file, CancellationToken cancellationToken = default)
	{
		if (!ModelState.IsValid)
		{
			return View(nameof(Edit), category);
		}

		if (category.CategoryId == 0)
		{
			//add new 

			if (file != null)
			{
				category.ImageName = await _fileService.UploadImageAsync(file, "Categories", cancellationToken);
			}
			else
			{
				category.ImageName = "x.png";
			}

			await _categoryService.AddAsync(category, cancellationToken);
		}

		else
		{
			//edit 
			if (file != null)
			{
				category.ImageName = await _fileService.UploadImageAsync(file, "Categories", cancellationToken);
			}
			else
			{
				category.ImageName = await _categoryService.GetImageFileAsync(category.CategoryId, cancellationToken);
			}
			await _categoryService.UpdateAsync(category.CategoryId, category, cancellationToken);
		}

		return RedirectToAction(nameof(List));
	}


}

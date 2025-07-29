namespace LapShop.MVC.Areas.Admin.Controllers;

[Area(SharedData.AdminArea)]
public class ItemController(
	IItemService itemService,
	ICategoryService categoryService,
	IItemTypeService itemTypeService,
	IOsService osService,
	IFileService fileService
	) : Controller
{
	private readonly IItemService _itemService = itemService;
	private readonly ICategoryService _categoryService = categoryService;
	private readonly IItemTypeService _itemTypeService = itemTypeService;
	private readonly IOsService _osService = osService;
	private readonly IFileService _fileService = fileService;

	public async Task<IActionResult> List(CancellationToken cancellationToken = default)
	{
		ViewBag.Categories = await _categoryService.GetAllInShortAsync(cancellationToken); ;
		ViewBag.ItemTypes = await _itemTypeService.GetAllInShortAsync(cancellationToken);

		// Save selected values to ViewData or ViewBag
		ViewBag.SelectedCategoryId = null;
		ViewBag.SelectedItemTypeId = null;

		var result = await _itemService.GetAllItemsDataAsync(cancellationToken: cancellationToken);
		return View(result);
	}

	public async Task<IActionResult> Search(int? categoryId, int? itemTypeId, CancellationToken cancellationToken = default)
	{
		ViewBag.Categories = await _categoryService.GetAllInShortAsync(cancellationToken);
		ViewBag.ItemTypes = await _itemTypeService.GetAllInShortAsync(cancellationToken);


		// Save selected values to ViewData or ViewBag
		ViewBag.SelectedCategoryId = (categoryId.HasValue) ? categoryId : null;
		ViewBag.SelectedItemTypeId = (itemTypeId.HasValue) ? itemTypeId : null;

		var result = await _itemService.GetAllItemsDataAsync(categoryId, itemTypeId, cancellationToken: cancellationToken);
		return View(nameof(List), result);
	}

	// as we know , add or edit 
	// add with no id 
	// edit with selected id 
	public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken = default)
	{

		ViewBag.Categories = await _categoryService.GetAllInShortAsync(cancellationToken);
		ViewBag.ItemTypes = await _itemTypeService.GetAllInShortAsync(cancellationToken);
		ViewBag.Os = await _osService.GetAllInShortAsync(cancellationToken);

		if (id == null)
		{
			//add new  
			return View(nameof(Edit), new TbItem());
		}
		else
		{
			//edit 
			var item = await _itemService.GetAsync(id!.Value, cancellationToken);
			return View(nameof(Edit), item);
		}
	}


	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Save(TbItem item, IFormFile? file, CancellationToken cancellationToken = default)
	{

		if (!ModelState.IsValid)
		{
			ViewBag.Categories = await _categoryService.GetAllInShortAsync(cancellationToken);
			ViewBag.ItemTypes = await _itemTypeService.GetAllInShortAsync(cancellationToken);
			ViewBag.Os = await _osService.GetAllInShortAsync(cancellationToken);

			return View(nameof(Edit), item);
		}


		if (item.ItemId == 0)
		{
			if (file != null)
			{
				item.ImageName = await _fileService.UploadImageAsync(file, "Items", cancellationToken);
			}
			else
			{
				item.ImageName = "x.png";
			}

			await _itemService.AddAsync(item, cancellationToken);
		}
		else
		{
			if (file != null)
			{
				item.ImageName = await _fileService.UploadImageAsync(file, "Items", cancellationToken);
			}
			else
			{
				item.ImageName = await _itemService.GetImageFileAsync(item.ItemId, cancellationToken);
			}

			await _itemService.UpdateAsync(item.ItemId, item, cancellationToken);
		}

		return RedirectToAction(nameof(List));

	}


	public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
	{
		var result = await _itemService.DeleteAsync(id, cancellationToken);
		return RedirectToAction(nameof(List));
	}



}

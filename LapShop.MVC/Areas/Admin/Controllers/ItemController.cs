namespace LapShop.MVC.Areas.Admin.Controllers;

[Area(SharedData.AdminArea)]
public class ItemController(
	IItemService itemService,
	ICategoryService categoryService,
	IItemTypeService itemTypeService) : Controller
{
	private readonly IItemService _itemService = itemService;
	private readonly ICategoryService _categoryService = categoryService;
	private readonly IItemTypeService _itemTypeService = itemTypeService;

	public async Task<IActionResult> List(CancellationToken cancellationToken=default)
	{
		ViewBag.Categories = await _categoryService.GetAllAsync();
		ViewBag.ItemTypes = await _itemTypeService.GetAllItemTypesInShortAsync();

		// Save selected values to ViewData or ViewBag
		ViewBag.SelectedCategoryId =  null;
		ViewBag.SelectedItemTypeId =  null;

		var result = await _itemService.GetAllItemsDataAsync(cancellationToken: cancellationToken);
		return View(result); 
	}

	public async Task<IActionResult> Search(int? categoryId, int? itemTypeId, CancellationToken cancellationToken = default)
	{
		ViewBag.Categories = await _categoryService.GetAllAsync();
		ViewBag.ItemTypes = await _itemTypeService.GetAllItemTypesInShortAsync();


		// Save selected values to ViewData or ViewBag
		ViewBag.SelectedCategoryId = (categoryId.HasValue)? categoryId : null;
		ViewBag.SelectedItemTypeId = (itemTypeId.HasValue) ? itemTypeId : null;

		var result = await _itemService.GetAllItemsDataAsync(categoryId,itemTypeId,cancellationToken);
		return View(nameof(List) ,result);
	}

	
}

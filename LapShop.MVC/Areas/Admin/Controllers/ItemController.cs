namespace LapShop.MVC.Areas.Admin.Controllers;

[Area(SharedData.AdminArea)]
public class ItemController(IItemService itemService) : Controller
{
	private readonly IItemService _itemService = itemService;

	public async Task<IActionResult> List(CancellationToken cancellationToken=default)
	{
		var result = await _itemService.GetAllItemsDataAsync(cancellationToken);
		return View(result);
	}
}

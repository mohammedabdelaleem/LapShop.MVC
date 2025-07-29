using Microsoft.AspNetCore.Mvc;

namespace LapShop.MVC.Controllers;
public class ItemController(IItemService itemService) : Controller
{
	private readonly IItemService _itemService = itemService;

	public async Task<IActionResult> Details(int id, CancellationToken cancellationToken = default)
	{
		var item = await _itemService.GetItemResponseAsync(id, cancellationToken);
		return View(nameof(Details), item);
	}
}

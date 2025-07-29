using Microsoft.AspNetCore.Mvc;

namespace LapShop.MVC.Controllers;
public class ItemController(
	IItemService itemService,
	IItemImagesService itemImagesService) : Controller
{
	private readonly IItemService _itemService = itemService;
	private readonly IItemImagesService _itemImagesService = itemImagesService;

	public async Task<IActionResult> Details(int id, CancellationToken cancellationToken = default)
	{

		VmItemDetails model = new()
		{
			Item = await _itemService.GetItemResponseAsync(id, cancellationToken),
			ItemImages = await _itemImagesService.GetImagesAsync(id, cancellationToken),
			RelatedItems = await _itemService.GetRelatedItems(id, cancellationToken: cancellationToken)
		};
		
		return View(nameof(Details), model);
	}
}

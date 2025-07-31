using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LapShop.MVC.Controllers;
public class OrderController(IItemService itemService) : Controller
{
	private readonly IItemService _itemService = itemService;
	private readonly string _sessionKey = "Cart";

	public IActionResult Cart()
	{
		var sessionCart = HttpContext.Session.GetString(_sessionKey) ?? string.Empty;

		var shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(sessionCart);

		return View(nameof(Cart), shoppingCart);
	}

	public async Task<IActionResult> AddToCart(int itemId, CancellationToken cancellationToken = default)
	{

		var item = await _itemService.GetAsync(itemId, cancellationToken);

		ShoppingCart cart;
		if(HttpContext.Session.GetString(_sessionKey) == null)
		{
			cart = new ShoppingCart();
		}else
		{
			cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Session.GetString(_sessionKey));
		}

		if(cart.Items.Any(x=>x.ItemId == itemId))
		{
			var cartItem =
			cart.Items
				.FirstOrDefault((x => x.ItemId == itemId))!;

			cartItem.Quantity ++;
			cartItem.Total = cartItem.Price * cartItem.Quantity;
		}else
		{
			cart.Items.Add(
		new ShoppingCartItem()
				{
					ItemId = itemId,
					ItemName = item.ItemName,
					IamgeName = item.ImageName,
					Quantity = 1,
					Price = item.SalesPrice,
					Total = item.SalesPrice,  // Qty = 1 at first 
				});

		}


		cart.Total = cart.Items.Sum(x => x.Total);
		HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));

		return RedirectToAction(nameof(Cart));
	}


}

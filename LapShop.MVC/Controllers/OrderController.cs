using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LapShop.MVC.Controllers;
public class OrderController(IItemService itemService) : Controller
{
	private readonly IItemService _itemService = itemService;
	private readonly string _stateManagementKey = "Cart";

	// session when you need to save the data during the session with the user opening the system [server side]
	// cookies when you need to save data for a long time, around [10-15] days [client side]
	// DB for Persistent Storing 

	// No Login : saved At Cookies
	// Login : get data from Db And Store it into cookies 

	// mustn't store sensitive data at cookies 


	public IActionResult Cart()
	{
		var cart = HttpContext.Request.Cookies[_stateManagementKey] ?? string.Empty;
		ShoppingCart shoppingCart;

		if (cart == string.Empty)
		{
			shoppingCart = new ShoppingCart();
		}
		else
		{
			shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(cart);
		}

		return View(nameof(Cart), shoppingCart);
	}

	public async Task<IActionResult> AddToCart(int itemId, CancellationToken cancellationToken = default)
	{

		var item = await _itemService.GetAsync(itemId, cancellationToken);

		ShoppingCart cart;
		if(HttpContext.Request.Cookies[_stateManagementKey] == null)
		{
			cart = new ShoppingCart();
		}else
		{
			cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies[_stateManagementKey]);
		}

		if(cart.Items.FirstOrDefault(x => x.ItemId == itemId) is { } cartItem )
		{

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
		HttpContext.Response.Cookies.Append(_stateManagementKey, JsonConvert.SerializeObject(cart),
			new CookieOptions
		{
			Expires = DateTimeOffset.Now.AddDays(7), 
			HttpOnly = true, // optional for added security
			Secure = true // optional if you use HTTPS
		});

		return RedirectToAction(nameof(Cart));
	}


	public IActionResult MyOrders()
	{
		return View();
	}


	[Authorize]
	public IActionResult OrderSuccess()
	{
		return View();
	}
}

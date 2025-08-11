
namespace LapShop.MVC.Controllers;
public class OrderController(
	IItemService itemService,
	ISalesInvoiceService salesInvoiceService) : Controller
{
	private readonly IItemService _itemService = itemService;
	private readonly ISalesInvoiceService _salesInvoiceService = salesInvoiceService;
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
	public async Task<IActionResult> OrderSuccess()
	{
		string sessionCart = string.Empty;
		if (HttpContext.Request.Cookies[_stateManagementKey] != null)
		{
			sessionCart = HttpContext.Request.Cookies[_stateManagementKey]!;
		}

		var cart = JsonConvert.DeserializeObject<ShoppingCart>(sessionCart);
		await SaveOrder(cart);


		return View();
	}

	private async Task SaveOrder(ShoppingCart? cart, CancellationToken cancellationToken= default)
	{
		try { 
		
			// fill sales invoice items List
			var lstInvoiceItems = new List<TbSalesInvoiceItem>();
			foreach (var cartItem in cart?.Items!)
			{
				lstInvoiceItems.Add(new TbSalesInvoiceItem
				{
					ItemId = cartItem.ItemId,
					Qty = cartItem.Quantity,
					InvoicePrice = cartItem.Price,
				});
			}

			// assign sales items into a new invoice
			var salesInvoiceObj = new TbSalesInvoice() { 
			CreatedDate = DateTime.UtcNow,
			CreatedBy = User.GetUserId()!,
			CustomerId = 3,
			DelivryDate = DateTime.UtcNow.AddDays(5),

			//TbSalesInvoiceItems = lstInvoiceItems, // when adding sales invoice it will adding this list , but this ok when adding new invoice but what if we need to update [comparing 2 lists]
			};

			await _salesInvoiceService.Save(salesInvoiceObj, lstInvoiceItems, true, User.GetUserId()!, cancellationToken);

		}	
		catch (Exception e){
		
		}
	}
}

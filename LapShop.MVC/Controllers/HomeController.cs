
namespace LapShop.MVC.Controllers
{
    public class HomeController(IItemService itemService) : Controller
    {
		private readonly IItemService _itemService = itemService;

		public async Task<IActionResult> Index(CancellationToken cancellationToken=default)
        {
			// simulation , but this depends on buisiness
			

			var items = await _itemService.GetAllItemsDataAsync(size:50,cancellationToken: cancellationToken);

			var model = new VmHomePage
			{
				AllItems = items.Take(10).ToList(),
				RecommendedItems = items.Skip(50).Take(10).ToList(),
				NewItems = items.SkipLast(40).TakeLast(10).ToList(),
				FreeDelivery = items.TakeLast(4).ToList(),
				FeautureItems = items.SkipLast(90).TakeLast(10).ToList(),
				//Categories = 
			};





			return View(model);

        }

      
		public IActionResult Error()
		{
			return View();
		}

	}
}

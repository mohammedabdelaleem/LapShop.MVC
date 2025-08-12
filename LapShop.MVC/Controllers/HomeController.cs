
namespace LapShop.MVC.Controllers
{
    public class HomeController(
		IItemService itemService,
		ISliderService sliderService,
		ICategoryService categoryService) : Controller
    {
		private readonly IItemService _itemService = itemService;
		private readonly ISliderService _sliderService = sliderService;
		private readonly ICategoryService _categoryService = categoryService;

		public async Task<IActionResult> Index(CancellationToken cancellationToken=default)
        {
			// simulation , but this depends on buisiness
			var items = await _itemService.GetAllItemsDataAsync(size:50,cancellationToken: cancellationToken);

			var model = new VmHomePage
			{
				AllItems = items.Take(10).ToList(),
				RecommendedItems = items.Skip(8).Take(10).ToList(),
				NewItems = items.SkipLast(3).Take(10).ToList(),
				FreeDelivery = items.TakeLast(4).ToList(),
				FeautureItems = items.Skip(7).TakeLast(10).ToList(),
				Sliders = await _sliderService.GetAllInShortAsync(cancellationToken),
				Categories = await _categoryService.GetAllInShortAsync(4,cancellationToken)
			};


			return View(model);

        }

      
		public IActionResult Error()
		{
			return View();
		}

	}
}

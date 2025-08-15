namespace LapShop.MVC.ApiControllers;
[Route("api/[controller]")]
[ApiController]
public class ItemsController(IItemService itemService) : ControllerBase
{
	private readonly IItemService _itemService = itemService;

	[HttpGet]
	public async Task<List<ItemResponse>> GetAll(CancellationToken cancellationToken = default)
	{
		var result = await _itemService.GetAllItemsDataAsync(cancellationToken: cancellationToken);
		return result;
	}

	[HttpGet("category/{categoryId}")]
	public async Task<List<ItemResponse>> GetAll(int categoryId, CancellationToken cancellationToken = default)
	{
		var result = await _itemService.GetAllItemsDataAsync(categoryId: categoryId, cancellationToken: cancellationToken);
		return result;
	}

	[HttpGet("{id}")]
	public async Task<ItemResponse> Get(int id, CancellationToken cancellationToken = default)
	{
		var result = await _itemService.GetItemResponseAsync(id,cancellationToken: cancellationToken);
		return result;
	}

	




}

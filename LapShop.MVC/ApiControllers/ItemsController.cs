namespace LapShop.MVC.ApiControllers;
[Route("api/[controller]")]
[ApiController]
public class ItemsController(IItemService itemService) : ControllerBase
{
	private readonly IItemService _itemService = itemService;

	[HttpGet]
	public async Task<List<ItemResponse>> GetAll(CancellationToken cancellationToken = default)
	{
		var result = await _itemService.GetAllItemsDataAsync(size: 300, cancellationToken: cancellationToken);
		return result;
	}

	[HttpGet("category/{categoryId}")]
	public async Task<List<ItemResponse>> GetAll([FromRoute] int categoryId, CancellationToken cancellationToken = default)
	{
		var result = await _itemService.GetAllItemsDataAsync(categoryId: categoryId, cancellationToken: cancellationToken);
		return result;
	}

	[HttpGet("{id}")]
	public async Task<ItemResponse> Get([FromRoute] int id, CancellationToken cancellationToken = default)
	{
		var result = await _itemService.GetItemResponseAsync(id,cancellationToken: cancellationToken);
		return result;
	}

	[HttpPost]
	public async Task Save([FromBody]TbItem item , CancellationToken cancellationToken = default)
	{
		await _itemService.SaveAsync(item, /*User.GetUserId()*/ "1", cancellationToken);
	}

	[HttpDelete("{id}")]
	public async Task<bool> Delete([FromRoute]int id, CancellationToken cancellationToken = default)
	{
		return await _itemService.DeleteAsync(id, cancellationToken);
	}

}


namespace LapShop.MVC.Services;

public interface IItemService
{
	Task<List<TbItem>> GetAllAsync(CancellationToken cancellationToken = default);
	Task<List<VwItem>> GetAllItemsDataAsync(int? categoryId = default, int? itemTypeId = default, CancellationToken cancellationToken = default);

	Task<TbItem?> GetAsync(int id, CancellationToken cancellationToken = default);
	Task AddAsync(TbItem item, CancellationToken cancellationToken = default);
	Task<bool> UpdateAsync(int itemId, TbItem item, CancellationToken cancellationToken);
	Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
	Task<string> GetImageFileAsync(int itemId, CancellationToken cancellationToken);
}

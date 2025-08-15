

namespace LapShop.MVC.Services;

public interface IItemService
{
	Task<List<TbItem>> GetAllAsync(CancellationToken cancellationToken = default);
	Task<List<ItemResponse>> GetAllItemsDataAsync(int? categoryId = default, int? itemTypeId = default,int size=10, CancellationToken cancellationToken = default);
	Task<List<ItemResponse>> GetRelatedItems(int itemId  ,int size = 10, CancellationToken cancellationToken = default);

	Task<TbItem?> GetAsync(int id, CancellationToken cancellationToken = default);
	Task<ItemResponse?> GetItemResponseAsync(int id, CancellationToken cancellationToken = default);
	Task AddAsync(TbItem item, CancellationToken cancellationToken = default);
	Task<bool> UpdateAsync(int itemId, TbItem item, CancellationToken cancellationToken);
	Task<bool> SaveAsync(TbItem item, string userId, CancellationToken cancellationToken = default);
	Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
	Task<string> GetImageFileAsync(int itemId, CancellationToken cancellationToken);
}

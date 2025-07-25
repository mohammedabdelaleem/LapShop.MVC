using LapShop.MVC.Contracts;

namespace LapShop.MVC.Services;

public interface IItemTypeService
{
	Task<List<ItemTypeResponse>> GetAllInShortAsync(CancellationToken cancellationToken=default);
	Task<List<TbItemType>> GetAllAsync(CancellationToken cancellationToken = default);
	Task<TbItemType> GetAsync(int id, CancellationToken cancellationToken = default);
	Task AddAsync(TbItemType itemType, CancellationToken cancellationToken);
	Task<bool> UpdateAsync(int itemTypeId, TbItemType updatedCategory, CancellationToken cancellationToken);
	Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

}

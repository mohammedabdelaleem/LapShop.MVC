using LapShop.MVC.Contracts;

namespace LapShop.MVC.Services;

public interface IItemTypeService
{
  Task<List<ItemTypeResponse>> GetAllItemTypesInShortAsync(CancellationToken cancellationToken=default);
}

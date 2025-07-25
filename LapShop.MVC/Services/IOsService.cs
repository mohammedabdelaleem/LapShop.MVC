using LapShop.MVC.Contracts;

namespace LapShop.MVC.Services;

public interface IOsService
{

	Task<List<OsResponse>> GetAllInShortAsync(CancellationToken cancellationToken = default);
	Task<List<TbO>> GetAllAsync(CancellationToken cancellationToken = default);
	Task<TbO> GetAsync(int id, CancellationToken cancellationToken = default);
	Task AddAsync(TbO os, CancellationToken cancellationToken);
	Task<bool> UpdateAsync(int osId, TbO updatedCategory, CancellationToken cancellationToken);
	Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}

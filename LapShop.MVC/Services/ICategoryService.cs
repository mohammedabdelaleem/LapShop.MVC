using LapShop.MVC.Models;

namespace LapShop.MVC.Services;

public interface ICategoryService
{
	Task<List<TbCategory>> GetAllAsync(CancellationToken cancellationToken = default);
	Task<TbCategory> GetAsync(int id, CancellationToken cancellationToken = default);

	Task AddAsync(TbCategory category, CancellationToken cancellationToken = default);
	Task<bool> UpdateAsync(int categoryId, TbCategory category, CancellationToken cancellationToken);
}

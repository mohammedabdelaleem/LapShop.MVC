using LapShop.MVC.Models;

namespace LapShop.MVC.Services;

public interface ICategoryService
{
	Task<List<TbCategory>> GetAll(CancellationToken cancellationToken = default);
}

using LapShop.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LapShop.MVC.Services;

public class CategoryService(AppDBContext context) : ICategoryService
{
	private readonly AppDBContext _context = context;

	public async Task<List<TbCategory>> GetAll(CancellationToken cancellationToken=default)
	{
		return await _context.TbCategories.ToListAsync(cancellationToken);
	}
}

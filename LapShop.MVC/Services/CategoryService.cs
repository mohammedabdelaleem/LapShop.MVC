using LapShop.MVC.Contracts;
using LapShop.MVC.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LapShop.MVC.Services;

public class CategoryService(AppDBContext context) : ICategoryService
{
	private readonly AppDBContext _context = context;


	public async Task<List<CategoryResponse>> GetAllInShortAsync(CancellationToken cancellationToken = default) =>
			await _context.TbCategories
					.ProjectToType<CategoryResponse>()
					.ToListAsync(cancellationToken);


	public async Task<List<TbCategory>> GetAllAsync(CancellationToken cancellationToken = default)
			=> await _context.TbCategories.ToListAsync(cancellationToken);

	public async Task<TbCategory> GetAsync(int id, CancellationToken cancellationToken = default) => await _context.TbCategories.SingleOrDefaultAsync(x => x.CategoryId == id, cancellationToken);

	public async Task AddAsync(TbCategory category, CancellationToken cancellationToken)
	{
		category.CreatedDate = DateTime.UtcNow;
		category.CreatedBy = "1";

		await _context.TbCategories.AddAsync(category, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

	}

	public async Task<bool> UpdateAsync(int categoryId, TbCategory updatedCategory, CancellationToken cancellationToken)
	{
		if (categoryId != updatedCategory.CategoryId || updatedCategory == null)
			return false;

		var categoryDB = await GetAsync(categoryId, cancellationToken);

		updatedCategory.Adapt(categoryDB);

		categoryDB.UpdatedDate = DateTime.UtcNow;
		categoryDB.UpdatedBy = "1";
		_context.Entry(categoryDB).State = EntityState.Modified;

		if (categoryDB.ImageName == null)
			categoryDB.ImageName = string.Empty;

		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

	public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		 _context.Remove(await GetAsync(id, cancellationToken));	
		 await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

	public async Task<string> GetImageFileAsync(int categoryId, CancellationToken cancellationToken)
	{
		var category = await GetAsync(categoryId, cancellationToken);

		return (category != null && !string.IsNullOrEmpty(category.ImageName)) ? 
					 category.ImageName : "x.png";
		
	}
}

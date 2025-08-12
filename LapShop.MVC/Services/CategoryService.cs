using LapShop.MVC.Contracts;
using LapShop.MVC.Persistance;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LapShop.MVC.Services;

public class CategoryService(AppDBContext context) : ICategoryService
{
	private readonly AppDBContext _context = context;


	public async Task<List<CategoryResponse>> GetAllInShortAsync(int size=100 ,CancellationToken cancellationToken = default) =>
			await _context.TbCategories
		.Where(x => x.CurrentState != 0)
		.OrderBy(x=>x.CategoryName)
		.Take(size)
		.ProjectToType<CategoryResponse>()
		.ToListAsync(cancellationToken);


	public async Task<List<TbCategory>> GetAllAsync(CancellationToken cancellationToken = default)
			=> await _context.TbCategories.Where(x => x.CurrentState != 0).ToListAsync(cancellationToken);

	public async Task<TbCategory> GetAsync(int id, CancellationToken cancellationToken = default) 
		=> await _context.TbCategories.SingleOrDefaultAsync(x => x.CategoryId == id && x.CurrentState !=0, cancellationToken);

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
		//_context.Entry(categoryDB).State = EntityState.Modified;

		if (categoryDB.ImageName == null)
			categoryDB.ImageName = string.Empty;

		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

	public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		 var model = await GetAsync(id, cancellationToken);
		 model.CurrentState = 0;

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

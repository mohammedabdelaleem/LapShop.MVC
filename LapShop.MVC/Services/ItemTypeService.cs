using LapShop.MVC.Contracts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LapShop.MVC.Services;

public class ItemTypeService(AppDBContext context) : IItemTypeService
{
	private readonly AppDBContext _context = context;

	public async Task<List<ItemTypeResponse>> GetAllInShortAsync(CancellationToken cancellationToken = default) =>
			await _context.TbItemTypes
					.ProjectToType<ItemTypeResponse>()
					.ToListAsync(cancellationToken);


	public async Task<List<TbItemType>> GetAllAsync(CancellationToken cancellationToken = default)
			=> await _context.TbItemTypes.ToListAsync(cancellationToken);

	public async Task<TbItemType> GetAsync(int id, CancellationToken cancellationToken = default) 
			=> await _context.TbItemTypes.SingleOrDefaultAsync(x => x.ItemTypeId == id, cancellationToken);

	public async Task AddAsync(TbItemType itemType, CancellationToken cancellationToken)
	{
		itemType.CreatedDate = DateTime.UtcNow;
		itemType.CreatedBy = "1";

		await _context.TbItemTypes.AddAsync(itemType, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

	}

	public async Task<bool> UpdateAsync(int itemTypeId, TbItemType updatedCategory, CancellationToken cancellationToken)
	{
		if (itemTypeId != updatedCategory.ItemTypeId || updatedCategory == null)
			return false;

		var itemTypeDB = await GetAsync(itemTypeId, cancellationToken);

		updatedCategory.Adapt(itemTypeDB);

		itemTypeDB.UpdatedDate = DateTime.UtcNow;
		itemTypeDB.UpdatedBy = "1";
		_context.Entry(itemTypeDB).State = EntityState.Modified;

		if (itemTypeDB.ImageName == null)
			itemTypeDB.ImageName = string.Empty;

		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

	public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		_context.Remove(await GetAsync(id, cancellationToken));
		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

}

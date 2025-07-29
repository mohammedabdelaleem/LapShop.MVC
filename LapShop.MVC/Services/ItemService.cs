using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LapShop.MVC.Services;

public class ItemService(AppDBContext context) : IItemService
{
	private readonly AppDBContext _context = context;

	public async Task<List<TbItem>> GetAllAsync(CancellationToken cancellationToken = default)
			=> await _context.TbItems.Where(x => x.CurrentState != 0).ToListAsync(cancellationToken);

	public async Task<List<ItemResponse>> GetAllItemsDataAsync(int? categoryId = default, int? itemTypeId = default,int size = 10,  CancellationToken cancellationToken = default)
	{

		return await _context.VwItems
				.Where(i =>
						(i.CategoryId == categoryId || categoryId == null || categoryId==0  ) &&
						(i.ItemTypeId == itemTypeId || itemTypeId == null || itemTypeId ==0 )&&
						 i.CurrentState == 1
						)
				.ProjectToType<ItemResponse>()
				.Take(size)
				.ToListAsync(cancellationToken);
	}


	public async Task<TbItem?> GetAsync(int id, CancellationToken cancellationToken = default)
		=> await _context.TbItems.SingleOrDefaultAsync(x => x.ItemId == id && x.CurrentState != 0, cancellationToken);

	public async Task AddAsync(TbItem item, CancellationToken cancellationToken)
	{
		item.CreatedDate = DateTime.Now;
		item.CreatedBy = "1";

		await _context.TbItems.AddAsync(item, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

	}

	public async Task<bool> UpdateAsync(int itemId, TbItem updateditem, CancellationToken cancellationToken)
	{
		if (updateditem == null || itemId != updateditem.ItemId )
			return false;

		var itemDB = await GetAsync(itemId, cancellationToken);

		updateditem.Adapt(itemDB);

		itemDB!.UpdatedDate = DateTime.UtcNow;
		itemDB.UpdatedBy = "1";
		//_context.Entry(itemDB).State = EntityState.Modified;

		if (itemDB.ImageName == null)
			itemDB.ImageName = string.Empty;

		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

	public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		var model = await GetAsync(id, cancellationToken);
		model!.CurrentState = 0;
		
		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

	public async Task<string> GetImageFileAsync(int itemId, CancellationToken cancellationToken)
	{
		var item = await GetAsync(itemId, cancellationToken);

		return (item != null && !string.IsNullOrEmpty(item.ImageName)) ?
					 item.ImageName : "x.png";

	}
}

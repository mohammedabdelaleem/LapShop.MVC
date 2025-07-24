using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LapShop.MVC.Services;

public class ItemService(AppDBContext context) : IItemService
{
	private readonly AppDBContext _context = context;

	public async Task<List<TbItem>> GetAllAsync(CancellationToken cancellationToken = default)
			=> await _context.TbItems.ToListAsync(cancellationToken);

	public async Task<List<VwItem>> GetAllItemsDataAsync(CancellationToken cancellationToken = default)
		=> await _context.VwItems.ToListAsync(cancellationToken);


	public async Task<TbItem> GetAsync(int id, CancellationToken cancellationToken = default) 
		=> await _context.TbItems.SingleOrDefaultAsync(x => x.ItemId == id, cancellationToken)!;

	public async Task AddAsync(TbItem item, CancellationToken cancellationToken)
	{
		item.CreatedDate = DateTime.UtcNow;
		item.CreatedBy = "1";

		await _context.TbItems.AddAsync(item, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

	}

	public async Task<bool> UpdateAsync(int itemId, TbItem updateditem, CancellationToken cancellationToken)
	{
		if (itemId != updateditem.ItemId || updateditem == null)
			return false;

		var itemDB = await GetAsync(itemId, cancellationToken);

		updateditem.Adapt(itemDB);

		itemDB.UpdatedDate = DateTime.UtcNow;
		itemDB.UpdatedBy = "1";
		_context.Entry(itemDB).State = EntityState.Modified;

		if (itemDB.ImageName == null)
			itemDB.ImageName = string.Empty;

		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

	public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		_context.Remove(await GetAsync(id, cancellationToken));
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


using LapShop.MVC.Persistance;
using Microsoft.EntityFrameworkCore;

namespace LapShop.MVC.Services;

public class ItemImagesService(AppDBContext context) : IItemImagesService
{
	private readonly AppDBContext _context = context;

	public async Task<List<TbItemImage>> GetImagesAsync(int itemId, CancellationToken cancellationToken=default)
	{
		return await _context.TbItemImages
			.Where(x=>x.ItemId == itemId)
			.ToListAsync(cancellationToken);
	}
}

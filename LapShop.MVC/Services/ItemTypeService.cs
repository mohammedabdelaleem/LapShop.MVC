using LapShop.MVC.Contracts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LapShop.MVC.Services;

public class ItemTypeService(AppDBContext context) : IItemTypeService
{
	private readonly AppDBContext _context = context;

	public async Task<List<ItemTypeResponse>> GetAllItemTypesInShortAsync(CancellationToken cancellationToken = default) =>
			await _context.TbItemTypes
					.ProjectToType<ItemTypeResponse>()
					.ToListAsync(cancellationToken);
}

using LapShop.MVC.Contracts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LapShop.MVC.Services;

public class OsService(AppDBContext context) : IOsService
{
	private readonly AppDBContext _context = context;

	public async Task<List<OsResponse>> GetAllInShortAsync(CancellationToken cancellationToken = default) =>
			await _context.TbOs
					.ProjectToType<OsResponse>()
					.ToListAsync(cancellationToken);


	public async Task<List<TbO>> GetAllAsync(CancellationToken cancellationToken = default)
			=> await _context.TbOs.ToListAsync(cancellationToken);

	public async Task<TbO> GetAsync(int id, CancellationToken cancellationToken = default)
			=> await _context.TbOs.SingleOrDefaultAsync(x => x.OsId == id, cancellationToken);

	public async Task AddAsync(TbO os, CancellationToken cancellationToken)
	{
		os.CreatedDate = DateTime.UtcNow;
		os.CreatedBy = "1";

		await _context.TbOs.AddAsync(os, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

	}

	public async Task<bool> UpdateAsync(int osId, TbO updatedCategory, CancellationToken cancellationToken)
	{
		if (osId != updatedCategory.OsId || updatedCategory == null)
			return false;

		var osDB = await GetAsync(osId, cancellationToken);

		updatedCategory.Adapt(osDB);

		osDB.UpdatedDate = DateTime.UtcNow;
		osDB.UpdatedBy = "1";
		_context.Entry(osDB).State = EntityState.Modified;

		if (osDB.ImageName == null)
			osDB.ImageName = string.Empty;

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

﻿using LapShop.MVC.Contracts;
using LapShop.MVC.Persistance;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LapShop.MVC.Services;

public class OsService(AppDBContext context) : IOsService
{
	private readonly AppDBContext _context = context;

	public async Task<List<OsResponse>> GetAllInShortAsync(CancellationToken cancellationToken = default) =>
			await _context.TbOs
					.Where(x=>x.CurrentState !=0)
					.ProjectToType<OsResponse>()
					.ToListAsync(cancellationToken);


	public async Task<List<TbO>> GetAllAsync(CancellationToken cancellationToken = default)
			=> await _context.TbOs.Where(x => x.CurrentState != 0).ToListAsync(cancellationToken);

	public async Task<TbO> GetAsync(int id, CancellationToken cancellationToken = default)
			=> await _context.TbOs.SingleOrDefaultAsync(x => x.OsId == id && x.CurrentState != 0, cancellationToken);

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
		//_context.Entry(osDB).State = EntityState.Modified;

		if (osDB.ImageName == null)
			osDB.ImageName = string.Empty;

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
}

namespace LapShop.MVC.Services;

public class SliderService(AppDBContext context) : ISliderService
{
	private readonly AppDBContext _context = context;

	public async Task<List<SliderResponse>> GetAllInShortAsync(CancellationToken cancellationToken = default) =>
			await _context.TbSliders
					.Where(x => x.CurrentState != 0)
					.ProjectToType<SliderResponse>()
					.ToListAsync(cancellationToken);


	public async Task<List<TbSlider>> GetAllAsync(CancellationToken cancellationToken = default)
			=> await _context.TbSliders.Where(x => x.CurrentState != 0).ToListAsync(cancellationToken);

	public async Task<TbSlider?> GetAsync(int id, CancellationToken cancellationToken = default) 
			=> await _context.TbSliders.SingleOrDefaultAsync(x => x.SliderId == id && x.CurrentState != 0, cancellationToken);

	public async Task AddAsync(TbSlider slider, CancellationToken cancellationToken)
	{
		slider.CreatedDate = DateTime.UtcNow;
		slider.CreatedBy = "1";

		await _context.TbSliders.AddAsync(slider, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

	}

	public async Task<bool> UpdateAsync(int sliderId, TbSlider updatedSlider, CancellationToken cancellationToken)
	{
		if (sliderId != updatedSlider.SliderId || updatedSlider == null)
			return false;

		var sliderDB = await GetAsync(sliderId, cancellationToken);

		updatedSlider.Adapt(sliderDB);

		sliderDB.UpdatedDate = DateTime.UtcNow;
		sliderDB.UpdatedBy = "1";

		if (sliderDB.ImageName == null)
			sliderDB.ImageName = "x.png";

		_context.Update(sliderDB);
		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

	public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		var model = await GetAsync(id, cancellationToken);

		if(model == null) return false;

		model.CurrentState = 0; 
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

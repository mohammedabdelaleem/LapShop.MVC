namespace LapShop.MVC.Services;

public interface ISliderService
{
	Task<List<SliderResponse>> GetAllInShortAsync(CancellationToken cancellationToken=default);
	Task<List<TbSlider>> GetAllAsync(CancellationToken cancellationToken = default);
	Task<TbSlider?> GetAsync(int id, CancellationToken cancellationToken = default);
	Task AddAsync(TbSlider slider, CancellationToken cancellationToken);
	Task<bool> UpdateAsync(int sliderId, TbSlider updatedCategory, CancellationToken cancellationToken);
	Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);

	Task<string> GetImageFileAsync(int itemId, CancellationToken cancellationToken);
}

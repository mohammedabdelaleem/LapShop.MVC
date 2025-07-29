namespace LapShop.MVC.Services;

public interface IItemImagesService
{
	Task<List<TbItemImage>> GetImagesAsync(int itemId, CancellationToken cancellationToken = default);
}

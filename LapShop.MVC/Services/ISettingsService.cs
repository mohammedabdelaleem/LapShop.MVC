
namespace LapShop.MVC.Services;

public interface ISettingsService
{
	Task<TbSettings?> GetAsync(CancellationToken cancellationToken = default);
	Task<bool> UpdateAsync(  TbSettings updatedCategory, CancellationToken cancellationToken);
	Task<string> GetImageFileAsync(SettingsPhotoes option, CancellationToken cancellationToken);
}

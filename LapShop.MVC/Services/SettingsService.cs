

namespace LapShop.MVC.Services;

public class SettingsService(AppDBContext context) : ISettingsService
{
	private readonly AppDBContext _context = context;

	public async Task<TbSettings> GetAsync(CancellationToken cancellationToken = default)
		=>await _context.Settings.FirstOrDefaultAsync(cancellationToken)!;


	public async Task<bool> UpdateAsync(TbSettings updatedSettings, CancellationToken cancellationToken)
	{
		var settingsDb = await GetAsync(cancellationToken);

		updatedSettings.Adapt(settingsDb);

		_context.Update(settingsDb);
		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}


	public async Task<string> GetImageFileAsync(SettingsPhotoes option, CancellationToken cancellationToken)
	{
		var settings = await GetAsync(cancellationToken);

		switch (option)
		{
		  case SettingsPhotoes.Logo:
				return (settings != null && !string.IsNullOrEmpty(settings.Logo)) ?
					 settings.Logo : "x.png";

			case SettingsPhotoes.MiddleBanner:
				return (settings != null && !string.IsNullOrEmpty(settings.MiddleBanner)) ?
					 settings.MiddleBanner : "x.png";


			case SettingsPhotoes.LastBanner:
				return (settings != null && !string.IsNullOrEmpty(settings.LastBanner)) ?
					 settings.LastBanner : "x.png";

		}

		return "x.png";
	}

}

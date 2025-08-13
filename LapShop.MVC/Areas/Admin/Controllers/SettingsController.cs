using System.Threading;

namespace LapShop.MVC.Areas.Admin.Controllers;

[Area(DefaultRoles.Admin)]
public class SettingsController(
	ISettingsService settingsService,
	IFileService fileService ) : Controller
{
	private readonly ISettingsService _settingsService = settingsService;
	private readonly IFileService _fileService = fileService;

	public async Task<IActionResult> List()
	{
		var settnigs = await _settingsService.GetAsync();
		return View(settnigs);
	}

	public async Task<IActionResult> Edit(CancellationToken cancellationToken = default) // we have only one record no need for id man 
	{
		return View(nameof(Edit) , await _settingsService.GetAsync(cancellationToken));
	}


	public async Task<IActionResult> Save(
				TbSettings model ,  
				IFormFile LogoFile, IFormFile MiddleBannerFile,
				IFormFile LastBannerFile,
				CancellationToken cancellationToken=default)
	{
		if (!ModelState.IsValid)
		{
			ModelState.AddModelError("", "Invalid Data");
			return View(nameof(Edit), model);
		}


		#region update images
		// logo
		if (LogoFile != null)
		{
			model.Logo = await _fileService.UploadImageAsync(LogoFile, "Settings", cancellationToken);
		}
		else
		{
			model.Logo = await _settingsService.GetImageFileAsync(SettingsPhotoes.Logo, cancellationToken);
		}


		//MiddleBanner
		if (MiddleBannerFile != null)
		{
			model.MiddleBanner = await _fileService.UploadImageAsync(MiddleBannerFile, "Settings", cancellationToken);
		}
		else
		{
			model.MiddleBanner = await _settingsService.GetImageFileAsync(SettingsPhotoes.MiddleBanner, cancellationToken);
		}


		//LastBanner
		if (LastBannerFile != null)
		{
			model.LastBanner = await _fileService.UploadImageAsync(LastBannerFile, "Settings", cancellationToken);
		}
		else
		{
			model.LastBanner = await _settingsService.GetImageFileAsync(SettingsPhotoes.LastBanner, cancellationToken);
		}
		#endregion

		await _settingsService.UpdateAsync(model, cancellationToken);

		return RedirectToAction(nameof(List));



	}
}

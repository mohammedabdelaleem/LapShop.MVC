using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.MVC.ApiControllers;
[Route("api/[controller]")]
[ApiController]
public class SettingController(ISettingsService settingsService) : ControllerBase
{
	private readonly ISettingsService _settingsService = settingsService;

	[HttpGet]
	public async Task<TbSettings> Get(CancellationToken cancellationToken= default)
	{
		 return await _settingsService.GetAsync(cancellationToken);
		//return Ok(result);
	}
}

namespace LapShop.MVC.Areas.Admin.Controllers;


[Area(DefaultRoles.Admin)]
public class SliderController(
	ISliderService sliderService,
	IFileService fileService) : Controller
{
	private readonly ISliderService _sliderService = sliderService;
	private readonly IFileService _fileService = fileService;

	public async Task<IActionResult> List(CancellationToken cancellationToken=default)
	{
		var sliders = await _sliderService.GetAllInShortAsync(cancellationToken);
		return View(sliders);
	}

	// Add new 
	// Edit(5)
	public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken=default)
	{
			return(id==null) ? 
					View(nameof(Edit) ,new TbSlider()): 
					View(nameof(Edit),await _sliderService.GetAsync(id!.Value, cancellationToken));
	}


	[ValidateAntiForgeryToken]
	[HttpPost]
	public async Task<IActionResult> Save(TbSlider item, IFormFile? file,CancellationToken cancellationToken = default)
	{
		if(!ModelState.IsValid) 
			return View(nameof(Edit), item);


		if(item.SliderId == 0)
		{
			if (file != null)
			{
				item.ImageName = await _fileService.UploadImageAsync(file, "Sliders", cancellationToken);
			}
			else
			{
				item.ImageName = "x.png";
			}

			await _sliderService.AddAsync(item, cancellationToken);
		}
		else
		{
			if (file != null)
			{
				item.ImageName = await _fileService.UploadImageAsync(file, "Sliders", cancellationToken);
			}
			else
			{
				item.ImageName = await _sliderService.GetImageFileAsync(item.SliderId, cancellationToken);
			}

			await _sliderService.UpdateAsync(item.SliderId ,item, cancellationToken);
		}

		return RedirectToAction(nameof(List));
	}


	public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
	{
		var result = await _sliderService.DeleteAsync(id, cancellationToken);
		return RedirectToAction(nameof(List));
	}

}

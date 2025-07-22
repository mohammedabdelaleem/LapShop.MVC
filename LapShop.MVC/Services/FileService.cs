namespace LapShop.MVC.Services;

public class FileService(IWebHostEnvironment webHostEnvironment) : IFileService
{
	

	public async Task<string> UploadImageAsync(IFormFile image, string savedFolderName, CancellationToken cancellationToken = default)
	{
		var uploadedPath = $"{webHostEnvironment.WebRootPath}/Uploads/{savedFolderName}";
		var fileName = Guid.CreateVersion7().ToString()+$"{Path.GetExtension(image.FileName)}" ;
		var	path = Path.Combine(uploadedPath, fileName);

		using var stream = File.Create(path);
		await image.CopyToAsync(stream, cancellationToken);	

		return fileName ;
	}
}

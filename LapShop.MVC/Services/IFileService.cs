namespace LapShop.MVC.Services;

public interface IFileService
{
	Task<string> UploadImageAsync(IFormFile image, string savedFolderName, CancellationToken cancellationToken = default);
}

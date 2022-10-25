namespace StoreApi.Services
{
	public interface IImageService
	{
		Task<string> SaveImageAsync(IFormFile file);

		Task<byte[]?> GetImageContentAsync(string fileName);

		string GetImageContentType();
	}
}

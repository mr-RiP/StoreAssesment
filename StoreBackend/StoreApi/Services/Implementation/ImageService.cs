namespace StoreApi.Services.Implementation
{
	public class ImageService : IImageService
	{
		private readonly static string[] ImageExtensions = { ".jpg", ".jpeg" };
		private readonly static string ImageContentType = "image/jpeg";
		private readonly static string ImageOriginalPath = Path.Combine(Directory.GetCurrentDirectory(), "Content/Original");
		private readonly static long MaxContentLength = 1024 * 1024; // 1 MB

		public ImageService()
		{
			if (!Directory.Exists(ImageOriginalPath))
			{
				Directory.CreateDirectory(ImageOriginalPath);
			}
		}

		public async Task<byte[]?> GetImageContentAsync(string fileName)
		{
			var filePath = Path.Combine(ImageOriginalPath, fileName);
			var fileInfo = new FileInfo(filePath);

			if (!fileInfo.Exists || ImageExtensions.Contains(fileInfo.Extension))
			{
				return null;
			}

			var content = await File.ReadAllBytesAsync(filePath);

			return content;
		}

		public string GetImageContentType()
		{
			return ImageContentType;
		}

		public async Task<string> SaveImageAsync(IFormFile file)
		{
			if (!ValidateImageFile(file))
			{
				throw new ArgumentOutOfRangeException();
			}

			var fileName = Guid.NewGuid().ToString() + ImageExtensions.First();
			var filePath = Path.Combine(ImageOriginalPath, fileName);

			using (var stream = new FileStream(filePath, FileMode.CreateNew))
			{
				await file.CopyToAsync(stream);
			}

			return fileName;
		}

		private bool ValidateImageFile(IFormFile file)
		{
			if (file.Length == 0 || file.Length > MaxContentLength || file.ContentType != ImageContentType)
			{
				return false;
			}
			else
			{
				var fileInfo = new FileInfo(file.FileName);

				if (!ImageExtensions.Contains(fileInfo.Extension))
				{
					return false;
				}
			}

			return true;
		}
	}
}

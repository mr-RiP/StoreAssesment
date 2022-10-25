using Microsoft.AspNetCore.Mvc;
using StoreApi.Services;

namespace StoreApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ImageController : ControllerBase
	{
		private readonly IImageService _service;

		public ImageController(IImageService service)
		{
			_service = service;
		}

		[HttpPost]
		[Route("Upload")]
		public async Task<ActionResult<string>> Upload([FromForm] IFormFile file)
		{
			try
			{
				var fileName = await _service.SaveImageAsync(file);

				return Ok(fileName);
			}
			catch (ArgumentOutOfRangeException)
			{
				return BadRequest();
			}
		}

		[HttpGet]
		[Route("Get/{fileName}")]
		public async Task<ActionResult> Get(string fileName)
		{
			var content = await _service.GetImageContentAsync(fileName);
			if (content == null)
			{
				return NotFound();
			}

			var contentType = _service.GetImageContentType();

			return File(content, contentType);
		}
	}
}

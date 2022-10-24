using Microsoft.AspNetCore.Mvc;
using StoreApi.Models;
using StoreApi.Repositories;

namespace StoreApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository _repository;

		public ProductController(IProductRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		[Route("Details/{id}")]
		public async Task<ActionResult<ProductDetails>> Details(int id, bool fullInfo = false)
		{
			if (id <= 0)
			{
				return BadRequest();
			}

			var result = await _repository.GetProductAsync(id, fullInfo);

			if (result == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(result);
			}
		}

		[HttpPost]
		[Route("Save")]
		public async Task<ActionResult<ProductDetails>> Save(ProductDetails model)
		{
			var result = await _repository.AddOrUpdateProductAsync(model);

			return Ok(result);
		}
	}
}

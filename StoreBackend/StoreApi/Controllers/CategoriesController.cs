using Microsoft.AspNetCore.Mvc;
using StoreApi.Models;
using StoreApi.Repositories;

namespace StoreApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoriesRepository _repository;

		public CategoriesController(ICategoriesRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		[Route("GetAll")]
		public async Task<ActionResult<CategoryList>> GetAll()
		{
			var result = await _repository.GetAllAsync();

			return Ok(result);
		}
	}
}

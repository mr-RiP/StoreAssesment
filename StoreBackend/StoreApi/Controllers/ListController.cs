using Microsoft.AspNetCore.Mvc;
using StoreApi.Enums;
using StoreApi.Models;
using StoreApi.Repositories;

namespace StoreApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ListController : ControllerBase
	{
		private readonly IListRepository _repository;

		private static readonly int PageSize = 20;
		private static readonly int MaxIdsLength = 100;

		public ListController(IListRepository listRepository)
		{
			_repository = listRepository;
		}

		[HttpGet]
		[Route("GetByPage/{page}")]
		public async Task<ActionResult<ProductList>> GetByPage(
			int page,
			int? category = null,
			bool availableOnly = true,
			ListSort sort = ListSort.NameAsc)
		{
			if (page < 1)
			{
				return BadRequest();
			}

			var result = await _repository.GetProductListByPageAsync(page, PageSize, availableOnly, category, sort);

			return Ok(result);
		}

		[HttpGet]
		[Route("GetByIds")]
		public async Task<ActionResult<ProductList>> GetByPage([FromQuery] int[] ids)
		{
			if (ids.Length == 0 || ids.Length > MaxIdsLength)
			{
				return BadRequest();
			}

			var result = await _repository.GetProductListByIdsAsync(ids);

			return Ok(result);
		}
	}
}

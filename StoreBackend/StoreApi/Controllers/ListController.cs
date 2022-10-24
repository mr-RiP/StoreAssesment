using Microsoft.AspNetCore.Mvc;
using StoreApi.Models;
using StoreDatabase;
using StoreDatabase.Enums;
using StoreDatabase.Models;
using System.Data;
using System.Data.Entity;

namespace StoreApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ListController : ControllerBase
	{
		private static int PageSize = 10;


		[Route("GetPage/{page}")]
		public async Task<ActionResult<ProductList>> GetPage(int page, int? category, bool? showAll)
		{
			if (page < 1)
			{
				return BadRequest();
			}

			using (var context = new StoreDatabaseContext())
			{
				IQueryable<Product> source = context.Products;

				if (showAll != true)
				{
					source = source.Where(p => p.Status == Status.Available);
				}

				if (category != null)
				{
					source = source.Where(p => p.CategoryId == category);
				}

				var itemArray = await source
					.OrderBy(p => p.Name)
					.Skip((page - 1) * PageSize)
					.Take(PageSize)
					.Select(p => new ProductListItem { Id = p.Id, Name = p.Name!, Price = p.Price })
					.ToArrayAsync();

				return new ProductList { Items = itemArray };
			}
		}
	}
}

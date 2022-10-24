using Microsoft.AspNetCore.Mvc;
using StoreApi.Models;
using StoreApi.Repositories;

namespace StoreApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CartController : ControllerBase
	{
		private readonly ICartRepository _repository;

		private static readonly int MaxItems = 100;

		public CartController(ICartRepository cartRepository)
		{
			_repository = cartRepository;
		}

		[HttpPost]
		[Route("Check")]
		public async Task<ActionResult<CartReport>> Check(CartItem[] items)
		{
			if (items == null || items.Length == 0 || items.Length > MaxItems)
			{
				return BadRequest();
			}
			
			try
			{
				var result = await _repository.CheckAsync(items);

				return Ok(result);
			}
			catch (ArgumentOutOfRangeException)
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("Order")]
		public async Task<ActionResult<CartReport>> Order(CartItem[] items)
		{
			if (items == null || items.Length == 0 || items.Length > MaxItems)
			{
				return BadRequest();
			}

			try
			{
				var result = await _repository.OrderAsync(items);

				return Ok(result);
			}
			catch (ArgumentOutOfRangeException)
			{
				return BadRequest();
			}
		}

	}
}

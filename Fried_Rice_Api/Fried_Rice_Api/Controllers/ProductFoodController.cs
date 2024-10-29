using Contracts;
using DomainModelEntity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fried_Rice_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ProductFoodController : ControllerBase
	{
		private IRepositoryWrapper _repoWrapper;
		public ProductFoodController(IRepositoryWrapper repoWrapper)
		{
			_repoWrapper = repoWrapper;
		}

		[HttpGet]
		public IEnumerable<ProductFood> GetAllProductFood()
		{
			return _repoWrapper.ProductFood.FindAll();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductFood>> GetProductFood(int id)
		{
			var account = await _repoWrapper.ProductFood.FindByCondition(e => e.ProductFoodId == id).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound();
			}
			return account;
		}

		[HttpPut("{id}")]
		public IActionResult PutProductFood(int id, ProductFood pr)
		{
			if (id != pr.ProductFoodId)
			{
				return BadRequest();
			}

			_repoWrapper.ProductFood.Update(pr);

			try
			{
				_repoWrapper.Save();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductFoodExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return NoContent();
		}

		[HttpPost]
		public ActionResult<ProductFood> PostProductFood(ProductFood pr)
		{
			_repoWrapper.ProductFood.Create(pr);
			_repoWrapper.Save();
			return CreatedAtAction("GetProductFood", new { id = pr.ProductFoodId }, pr);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProductFood(int id)
		{
			var pr = await _repoWrapper.ProductFood.FindByCondition(e => e.ProductFoodId == id).FirstOrDefaultAsync();
			if (pr == null)
			{
				return NotFound();
			}
			_repoWrapper.ProductFood.Delete(pr);
			_repoWrapper.Save();
			return NoContent();
		}

		private bool ProductFoodExists(int id)
		{
			return _repoWrapper.ProductFood.FindByCondition(e => e.ProductFoodId == id).Any(e => e.ProductFoodId == id);
		}
	}
}

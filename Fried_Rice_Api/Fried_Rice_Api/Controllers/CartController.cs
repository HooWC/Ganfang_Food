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
	public class CartController : ControllerBase
	{
		private IRepositoryWrapper _repoWrapper;
		public CartController(IRepositoryWrapper repoWrapper)
		{
			_repoWrapper = repoWrapper;
		}

		[HttpGet]
		public IEnumerable<Cart> GetAllCart()
		{
			return _repoWrapper.Cart.FindAll();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Cart>> GetCart(int id)
		{
			var account = await _repoWrapper.Cart.FindByCondition(e => e.CartId == id).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound();
			}
			return account;
		}

		[HttpPut("{id}")]
		public IActionResult PutCart(int id, Cart ca)
		{
			if (id != ca.CartId)
			{
				return BadRequest();
			}

			_repoWrapper.Cart.Update(ca);

			try
			{
				_repoWrapper.Save();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CartExists(id))
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
		public ActionResult<Cart> PostCart(Cart ca)
		{
			_repoWrapper.Cart.Create(ca);
			_repoWrapper.Save();
			return CreatedAtAction("GetCart", new { id = ca.CartId }, ca);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCart(int id)
		{
			var ca = await _repoWrapper.Cart.FindByCondition(e => e.CartId == id).FirstOrDefaultAsync();
			if (ca == null)
			{
				return NotFound();
			}
			_repoWrapper.Cart.Delete(ca);
			_repoWrapper.Save();
			return NoContent();
		}

		private bool CartExists(int id)
		{
			return _repoWrapper.Cart.FindByCondition(e => e.CartId == id).Any(e => e.CartId == id);
		}
	}
}

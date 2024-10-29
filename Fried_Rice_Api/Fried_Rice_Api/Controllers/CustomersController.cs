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
	public class CustomersController : ControllerBase
	{
		private IRepositoryWrapper _repoWrapper;
		public CustomersController(IRepositoryWrapper repoWrapper)
		{
			_repoWrapper = repoWrapper;
		}

		[HttpGet]
		public IEnumerable<Customers> GetAllCustomers()
		{
			return _repoWrapper.Customers.FindAll();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Customers>> GetCustomers(int id)
		{
			var account = await _repoWrapper.Customers.FindByCondition(e => e.CustomersId == id).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound();
			}
			return account;
		}

		[HttpPut("{id}")]
		public IActionResult PutCustomers(int id, Customers ca)
		{
			if (id != ca.CustomersId)
			{
				return BadRequest();
			}

			_repoWrapper.Customers.Update(ca);

			try
			{
				_repoWrapper.Save();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CustomersExists(id))
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
		public ActionResult<Customers> PostAdmin(Customers cu)
		{
			_repoWrapper.Customers.Create(cu);
			_repoWrapper.Save();
			return CreatedAtAction("GetCustomers", new { id = cu.CustomersId }, cu);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCustomers(int id)
		{
			var cu = await _repoWrapper.Customers.FindByCondition(e => e.CustomersId == id).FirstOrDefaultAsync();
			if (cu == null)
			{
				return NotFound();
			}
			_repoWrapper.Customers.Delete(cu);
			_repoWrapper.Save();
			return NoContent();
		}

		private bool CustomersExists(int id)
		{
			return _repoWrapper.Customers.FindByCondition(e => e.CustomersId == id).Any(e => e.CustomersId == id);
		}
	}
}

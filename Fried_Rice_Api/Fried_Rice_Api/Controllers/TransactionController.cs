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
	public class TransactionController : ControllerBase
	{
		private IRepositoryWrapper _repoWrapper;
		public TransactionController(IRepositoryWrapper repoWrapper)
		{
			_repoWrapper = repoWrapper;
		}

		[HttpGet]
		public IEnumerable<Transaction> GetAllTransaction()
		{
			return _repoWrapper.Transaction.FindAll();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Transaction>> GetTransaction(int id)
		{
			var tr = await _repoWrapper.Transaction.FindByCondition(e => e.TransactionId == id).FirstOrDefaultAsync();
			if (tr == null)
			{
				return NotFound();
			}
			return tr;
		}

		[HttpPut("{id}")]
		public IActionResult PutTransaction(int id, Transaction tr)
		{
			if (id != tr.TransactionId)
			{
				return BadRequest();
			}

			_repoWrapper.Transaction.Update(tr);

			try
			{
				_repoWrapper.Save();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TransactionExists(id))
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
		public ActionResult<Transaction> PostTransaction(Transaction tr)
		{
			_repoWrapper.Transaction.Create(tr);
			_repoWrapper.Save();
			return CreatedAtAction("GetTransaction", new { id = tr.TransactionId }, tr);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTransaction(int id)
		{
			var tr = await _repoWrapper.Transaction.FindByCondition(e => e.TransactionId == id).FirstOrDefaultAsync();
			if (tr == null)
			{
				return NotFound();
			}
			_repoWrapper.Transaction.Delete(tr);
			_repoWrapper.Save();
			return NoContent();
		}

		private bool TransactionExists(int id)
		{
			return _repoWrapper.Transaction.FindByCondition(e => e.TransactionId == id).Any(e => e.TransactionId == id);
		}
	}
}

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
	public class AdminController : ControllerBase
	{

		private IRepositoryWrapper _repoWrapper;
		public AdminController(IRepositoryWrapper repoWrapper)
		{
			_repoWrapper = repoWrapper;
		}

		[HttpGet]
		public IEnumerable<Admin> GetAllAdmin()
		{
			return _repoWrapper.Admin.FindAll();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Admin>> GetAdmin(int id)
		{
			var account = await _repoWrapper.Admin.FindByCondition(e => e.AdminID == id).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound();
			}
			return account;
		}

		[HttpPut("{id}")]
		public IActionResult PutAdmin(int id, Admin ad)
		{
			if (id != ad.AdminID)
			{
				return BadRequest();
			}

			_repoWrapper.Admin.Update(ad);

			try
			{
				_repoWrapper.Save();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AdminExists(id))
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
		public ActionResult<Admin> PostAdmin(Admin ad)
		{
			_repoWrapper.Admin.Create(ad);
			_repoWrapper.Save();
			return CreatedAtAction("GetAdmin", new { id = ad.AdminID }, ad);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAdmin(int id)
		{
			var ad = await _repoWrapper.Admin.FindByCondition(e => e.AdminID == id).FirstOrDefaultAsync();
			if (ad == null)
			{
				return NotFound();
			}
			_repoWrapper.Admin.Delete(ad);
			_repoWrapper.Save();
			return NoContent();
		}

		private bool AdminExists(int id)
		{
			return _repoWrapper.Admin.FindByCondition(e => e.AdminID == id).Any(e => e.AdminID == id);
		}
	}
}

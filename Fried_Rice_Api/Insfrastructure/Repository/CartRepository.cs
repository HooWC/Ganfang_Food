using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using DomainModelEntity.Models;

namespace Insfrastructure.Repository
{
	public class CartRepository : RepositoryBase<Cart>, ICart
	{
		public CartRepository(AppDbContext appDbContext) : base(appDbContext)
		{

		}
	}
}

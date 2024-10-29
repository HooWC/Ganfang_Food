using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using DomainModelEntity.Models;

namespace Insfrastructure.Repository
{
	public class CustomersRepository : RepositoryBase<Customers>, ICustomers
	{
		public CustomersRepository(AppDbContext appDbContext) : base(appDbContext)
		{

		}
	}
}

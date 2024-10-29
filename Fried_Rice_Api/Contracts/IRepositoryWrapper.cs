using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IRepositoryWrapper
	{
		IAdmin Admin { get; }
		ICart Cart { get; }
		ICustomers Customers { get; }
		IProductFood ProductFood { get; }
		ITransaction Transaction { get; }
		void Save();
	}
}

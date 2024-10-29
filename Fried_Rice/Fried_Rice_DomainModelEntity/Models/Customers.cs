using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fried_Rice_DomainModelEntity.Models
{
	public class Customers
	{
		public int CustomersId { get; set; }
		public string? CustomersIdNo { get; set; }
		public string? CustomersName { get; set; }
		public string? Email { get; set; }
		public string? Avatar { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public bool Active { get; set; }
	}
}

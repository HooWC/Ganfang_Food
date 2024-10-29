using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModelEntity.Models
{
	public class Cart
	{
		public int CartId { get; set; }
		public string? CardIdNo { get; set; }
		public string? ProductFoodIdNo { get; set; }
		public int Quantity { get; set; }
		public bool Status { get; set; }
		public string? CustomersIdNo { get; set; }
	}
}

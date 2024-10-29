using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModelEntity.Models
{
	public class Transaction
	{
		public int TransactionId { get; set; }
		public string? TransactionIdNo { get; set; }
		public string? CustomersIdNo { get; set; }
		public string? CartIdNo { get; set; }
		public string? Date { get; set; }
		public bool Status { get; set; }
	}
}

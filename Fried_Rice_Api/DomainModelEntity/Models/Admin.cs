using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModelEntity.Models
{
	public class Admin
	{
		public int AdminID { get; set; }
		public string? FullName { get; set; }
		public string? Avatar { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
	}
}

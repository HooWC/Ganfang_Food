using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fried_Rice_DomainModelEntity.Models
{
	public class ProductFood
	{
		public int ProductFoodId { get; set; }
		public string? ProductFoodIdNo { get; set; }
		public string? ProductFoodName { get; set; }
		public decimal Price { get; set; }
		public string? Type { get; set; }
		public int Quantity { get; set; }
		public bool Active { get; set; }
		public string? FoodImg { get; set; }
		public int Like { get; set; }
		public int Dislike { get; set; }
	}
}

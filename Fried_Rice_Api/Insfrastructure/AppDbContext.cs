using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModelEntity.Models;
using Microsoft.EntityFrameworkCore;

namespace Insfrastructure
{
	public class AppDbContext : DbContext
	{
		public AppDbContext()
		{

		}

		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Admin> Admins { get; set; } = null!;
		public DbSet<Cart> Carts { get; set; } = null!;
		public DbSet<Customers> Customer { get; set; } = null!;
		public DbSet<ProductFood> ProductFoods { get; set; } = null!;
		public DbSet<Transaction> Transactions { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=LAPTOP-75SCS0RS\\SQLEXPRESS;Database=FriedRice;Trusted_Connection=True;TrustServerCertificate=true");
			}
		}

	}
}

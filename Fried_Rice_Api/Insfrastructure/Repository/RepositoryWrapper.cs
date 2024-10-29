using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace Insfrastructure.Repository
{
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private AppDbContext DB;

		public RepositoryWrapper(AppDbContext _db)
		{
			DB = _db;
		}

		private IAdmin _admin;
		private ICart _cart;
		private ICustomers _customers;
		private IProductFood _productFood;
		private ITransaction _transaction;

		public IAdmin Admin
		{
			get
			{
				if (_admin == null)
				{
					_admin = new AdminRepository(DB);
				}
				return _admin;
			}
		}

		public ICart Cart
		{
			get
			{
				if (_cart == null)
				{
					_cart = new CartRepository(DB);
				}
				return _cart;
			}
		}

		public ICustomers Customers
		{
			get
			{
				if (_customers == null)
				{
					_customers = new CustomersRepository(DB);
				}
				return _customers;
			}
		}

		public IProductFood ProductFood
		{
			get
			{
				if (_productFood == null)
				{
					_productFood = new ProductFoodRepository(DB);
				}
				return _productFood;
			}
		}

		public ITransaction Transaction
		{
			get
			{
				if (_transaction == null)
				{
					_transaction = new TransactionRepository(DB);
				}
				return _transaction;
			}
		}

		public void Save()
		{
			DB.SaveChanges();
		}
	}
}

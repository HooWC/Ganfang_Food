using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fried_Rice_DomainModelEntity.Models;
using Newtonsoft.Json;

namespace Fried_Rice_Insfrastructure.Repository.Token
{
	public class CustomerToken
	{
		public static string Userid_Token = null;
		public static string Password_Token = null;


		public async Task<string> Token()
		{
			using (var httpClient = new HttpClient())
			{
				Customers cur = new Customers()
				{
					Username = Userid_Token,
					Password = Password_Token
				};

				string? token = null;
				StringContent content = new StringContent(JsonConvert.SerializeObject(cur), Encoding.UTF8, "application/json");
				using (var response = await httpClient.PostAsync("http://localhost:5198/api/CustomersToken", content))
				{
					token = await response.Content.ReadAsStringAsync();
				}
				return token;
			}
		}
	}
}

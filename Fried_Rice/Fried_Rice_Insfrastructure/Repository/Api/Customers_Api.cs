using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Fried_Rice_DomainModelEntity.Models;
using Fried_Rice_Insfrastructure.Repository.Token;
using Newtonsoft.Json;

namespace Fried_Rice_Insfrastructure.Repository.Api
{
	public class Customers_Api
	{
		const string baseUrl = "http://localhost:5198/api/";
		HttpClient client = new HttpClient();
		public AdminToken ad_token = new AdminToken();
		public CustomerToken cus_token = new CustomerToken();

		public async Task<List<Customers>> GetAllCustomers()
		{
			string accessToken = ad_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "Customers");
			return JsonConvert.DeserializeObject<List<Customers>>(jsonStr);
		}
		public async Task<List<Customers>> GetAllCustomers_cur()
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "Customers");
			return JsonConvert.DeserializeObject<List<Customers>>(jsonStr);
		}

		public async Task<Customers> FindCustomer(int id)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = await client.GetStringAsync(baseUrl + "Customers/" + id);
			return JsonConvert.DeserializeObject<Customers>(jsonStr);
		}

		public async void EditCustomer(Customers cur)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(cur), Encoding.UTF8, "application/json");
			await client.PutAsync(baseUrl + "Customers/" + cur.CustomersId, jsonStr);
		}

		public void DeleteCustomer(int id)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			client.DeleteAsync(baseUrl + "Customers/" + id);
		}

		public async void CustomerCreateData(Customers cur)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(cur), Encoding.UTF8, "application/json");
			await client.PostAsync(baseUrl + "Customers", jsonStr);
		}
	}
}

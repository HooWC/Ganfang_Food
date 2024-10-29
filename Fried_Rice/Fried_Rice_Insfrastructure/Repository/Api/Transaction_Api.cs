using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Fried_Rice_DomainModelEntity.Models;
using Fried_Rice_Insfrastructure.Repository.Token;
using Newtonsoft.Json;

namespace Fried_Rice_Insfrastructure.Repository.Api
{
	public class Transaction_Api
	{
		const string baseUrl = "http://localhost:5198/api/";
		HttpClient client = new HttpClient();
		public CustomerToken cus_token = new CustomerToken();

		public async Task<List<Transaction>> GetAllTransaction()
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = await client.GetStringAsync(baseUrl + "Transaction");
			return JsonConvert.DeserializeObject<List<Transaction>>(jsonStr);
		}

		public async Task<Transaction> GetTransactiont(int id)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = await client.GetStringAsync(baseUrl + "Transaction/" + id);
			return JsonConvert.DeserializeObject<Transaction>(jsonStr);
		}

		public async void TransactionCreate(Transaction tr)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(tr), Encoding.UTF8, "application/json");
			await client.PostAsync(baseUrl + "Transaction", jsonStr);
		}
	}
}

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
	public class ProductFood_Api
	{
		const string baseUrl = "http://localhost:5198/api/";
		HttpClient client = new HttpClient();
		public CustomerToken cus_token = new CustomerToken();

		public async Task<List<ProductFood>> GetAllProductFood()
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "ProductFood");
			return JsonConvert.DeserializeObject<List<ProductFood>>(jsonStr);
		}

		public async Task<ProductFood> GetProductFood(int id)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			string jsonStr = await client.GetStringAsync(baseUrl + "ProductFood/" + id);
			return JsonConvert.DeserializeObject<ProductFood>(jsonStr);
		}



	}
}

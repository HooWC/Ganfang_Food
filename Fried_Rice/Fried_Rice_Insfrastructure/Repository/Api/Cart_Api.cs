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
	public class Cart_Api
	{
		const string baseUrl = "http://localhost:5198/api/";
		HttpClient client = new HttpClient();
		public CustomerToken cus_token = new CustomerToken();

		public async Task<List<Cart>> GetAllCart()
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = await client.GetStringAsync(baseUrl + "Cart");
			return JsonConvert.DeserializeObject<List<Cart>>(jsonStr);
		}

		public async Task<Cart> GetCart(int id)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = await client.GetStringAsync(baseUrl + "Cart/" + id);
			return JsonConvert.DeserializeObject<Cart>(jsonStr);
		}

		public async void EditCart(Cart cart)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json");
			await client.PutAsync(baseUrl + "Cart/" + cart.CartId, jsonStr);
		}

		public void DeleteCustomer(int id)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			client.DeleteAsync(baseUrl + "Cart/" + id);
		}

		public async void CartCreate(Cart cart)
		{
			string accessToken = cus_token.Token().Result;
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var jsonStr = new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json");
			await client.PostAsync(baseUrl + "Cart", jsonStr);
		}


	}
}

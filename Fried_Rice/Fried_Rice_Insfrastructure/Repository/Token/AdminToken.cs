using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fried_Rice_DomainModelEntity.Models;
using Newtonsoft.Json;

namespace Fried_Rice_Insfrastructure.Repository.Token
{
	public class AdminToken
	{

		public async Task<string> Token()
		{
			using (var httpClient = new HttpClient())
			{
				Admin ad = new Admin()
				{
					Username = "Hoo6882",
					Password = "084487487"
				};

				string token = null;
				StringContent content = new StringContent(JsonConvert.SerializeObject(ad), Encoding.UTF8, "application/json");
				using (var response = await httpClient.PostAsync("http://localhost:5198/api/AdminToken", content))
				{
					token = await response.Content.ReadAsStringAsync();

				}
				return token;
			}
		}
	}
}

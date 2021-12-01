using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CryptoBalance.Models.Providers
{
    public class Provider
    {
        public List<CryptoCoin> ListOfCoins { get; set; }

        public Provider()
        {



        }

        public async Task LoadDataAsync(int numberOfCoins)
        {
            ListOfCoins = new List<CryptoCoin>();

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://coinlore-cryptocurrency.p.rapidapi.com/api/tickers/?start=0&limit=100"),
                Headers =
            {
            { "x-rapidapi-host", "coinlore-cryptocurrency.p.rapidapi.com" },
            { "x-rapidapi-key", "9c47f4ae44msh61e848dc5ad166dp1a46afjsnf6fccb01729e" },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                JObject joResponse = JObject.Parse(body);

                for (int i = 0; i < numberOfCoins; i++)
                {

                    int id = Convert.ToInt32(joResponse["data"][i]["id"]);
                    string name = Convert.ToString(joResponse["data"][i]["name"]);
                    int rank = Convert.ToInt32(joResponse["data"][i]["rank"]);
                    string symbol = Convert.ToString(joResponse["data"][i]["symbol"]);
                    double price_usd = Convert.ToDouble(joResponse["data"][i]["price_usd"]);
                    double percentage_change = Convert.ToDouble(joResponse["data"][i]["percent_change_24h"]);
                    
                    
                    CryptoCoin newCoin = new CryptoCoin(id,name, rank, symbol, price_usd, percentage_change);

                    ListOfCoins.Add(newCoin);
                
                }


            }
        }

    }
}
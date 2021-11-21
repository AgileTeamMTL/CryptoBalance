using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace CryptoBalance.Models.Providers
{
    public class Hitbtc
    {

        public static async Task<string> ShowCrypto()
        {
            string crypto = await RunAsync();
            return crypto;

        }

        static async Task<string> RunAsync()
        {
            
            using (var coin = new HttpClient())
            {
                coin.BaseAddress = new Uri("https://api.hitbtc.com/api/3/public/");
                coin.DefaultRequestHeaders.Accept.Clear();
                coin.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await coin.GetAsync("price/rate?from=ETH&to=BTC");
                if (response.IsSuccessStatusCode)
                {
                    //Coin crypto = await response.Content.ReadAsAsync<Coin>();

                     string crypto = await response.Content.ReadAsStringAsync();

                    //Console.WriteLine("{0}\t${1}\t{2}", crypto.Currency,crypto.Price, crypto.Timestamp);
                    
                }
                       
            }

            return "error";
        }


    }

   

}
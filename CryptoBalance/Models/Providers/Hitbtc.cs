using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using CryptoBalance.Models;

namespace CryptoBalance.Models.Providers
{
    public static class Hitbtc
    {

        public static async Task<TransactionModel> ShowCrypto(String from_currency, String to_currency)
        {
            string crypto = await RunAsync(from_currency, to_currency);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            TransactionModel transaction = serializer.Deserialize<TransactionModel>(crypto);
            return transaction;

        }

        static async Task<string> RunAsync(String from_currency, String to_currency)
        {
            //https://api.hitbtc.com/api/3/public/price/ticker/BTCUSDT

            using (var coin = new HttpClient())
            {

                coin.BaseAddress = new Uri("https://api.hitbtc.com/api/3/public/price/ticker/");
                coin.DefaultRequestHeaders.Accept.Clear();
                coin.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HttpResponseMessage response = await coin.GetAsync("BTCUSDT");
                HttpResponseMessage response = await coin.GetAsync(from_currency.ToUpper() + to_currency.ToUpper());
                if (response.IsSuccessStatusCode)
                {

                    string crypto = await response.Content.ReadAsStringAsync();
                    return crypto;

                }

            }

            return "error";
        }

    }



}
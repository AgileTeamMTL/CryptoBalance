using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CryptoBalance;
using CryptoBalance.Models;
using CryptoBalance.Models.Providers;
using Newtonsoft.Json.Linq;
using PagedList;

namespace CryptoBalance.Controllers
{
    public class HomeController : Controller
    {

        private db_cryptoBalanceEntities1 db = new db_cryptoBalanceEntities1();


        public async Task<ActionResult> Index()
        {
            HttpCookie cookie = Request.Cookies["AuthCookie"];
            if (cookie == null)
            {

                return RedirectToAction("SignIn", "users");

            }

            //Currency List
            List<SelectListItem> itemsFiat = new List<SelectListItem>();
            itemsFiat.Add(new SelectListItem { Text = "USD", Value = "0", Selected = true });
            itemsFiat.Add(new SelectListItem { Text = "CAD", Value = "1" });
            itemsFiat.Add(new SelectListItem { Text = "COP", Value = "2" });
            itemsFiat.Add(new SelectListItem { Text = "BRL", Value = "3" });
            ViewBag.FiatCurrencies = itemsFiat;

            //Crypto currency coins
            List<SelectListItem> itemsCrypto = new List<SelectListItem>();
            itemsCrypto.Add(new SelectListItem { Text = "BTC", Value = "BTC", Selected = true });
            itemsCrypto.Add(new SelectListItem { Text = "ETH", Value = "ETH" });
            itemsCrypto.Add(new SelectListItem { Text = "ADA", Value = "ADA" });
            itemsCrypto.Add(new SelectListItem { Text = "SOL", Value = "SOL" });
            itemsCrypto.Add(new SelectListItem { Text = "XRP", Value = "XRP" });
            ViewBag.CryptoCurrencies = itemsCrypto;

            //Loading crypto coins table
            Provider provider = new Provider();
            await provider.LoadDataAsync(10);

            List<CryptoCoin> listOfCoins = provider.ListOfCoins;

            ViewBag.listOfCoins = listOfCoins;

            
            var getTransactions = from tr in db.transactions
                                  where tr.username == cookie.Value
                                  select tr;

            if (getTransactions.ToList().Any())
            {

                ViewBag.transactions = getTransactions.ToList();

            }
            else {

                ViewBag.transactions = null;
            }

            
            
            //calculate balance
            double sumBTC = 0;
            double sumFiat = 0;

            foreach (var item in getTransactions)
            {
                sumBTC += Convert.ToDouble(item.cryto_total);
                sumFiat += Convert.ToDouble(item.amount);
            }

            //BitCoin ID = 90
            var CoinBTC = listOfCoins.Find(coin => coin.Id == 90);

            if (sumFiat > 0)
            {
                double balance = sumBTC * CoinBTC.CurrentPrice;
                double rate = ((balance - sumFiat) / (sumFiat)) * 100;

                ViewBag.rate = Math.Round(rate, 2);
                ViewBag.balance = Math.Round(balance, 2);
            }
            else {

                ViewBag.rate = 0;
                ViewBag.balance = 0;

            }


            return View();
        }
        public async Task<ActionResult> AllCoins(string searchString, int? page)
        {
            HttpCookie cookie = Request.Cookies["AuthCookie"];
            if (cookie == null)
            {

                return RedirectToAction("SignIn", "users");

            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            Provider provider = new Provider();
            await provider.LoadDataAsync(100);

            var listOfCoins = provider.ListOfCoins;

            if (!String.IsNullOrEmpty(searchString))
            {
                page = 1;
                searchString = char.ToUpper(searchString[0]) + searchString.Substring(1);
                var searchedCoins = listOfCoins.Where(coin => coin.Name.Contains(searchString));
                return View(searchedCoins.ToPagedList(pageNumber, pageSize));
            }


            return View(listOfCoins.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
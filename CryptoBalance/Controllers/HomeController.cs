using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CryptoBalance;
using CryptoBalance.Models;
using CryptoBalance.Models.Providers;

namespace CryptoBalance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public async Task<ActionResult> Dashboard()
        {
            TransactionModel bitcoin = await Hitbtc.ShowCrypto("BTC", "USDT");
            TransactionModel etherium = await Hitbtc.ShowCrypto("ETH", "USDT");
            TransactionModel solana = await Hitbtc.ShowCrypto("SOL", "USDT");
            TransactionModel cardano = await Hitbtc.ShowCrypto("ADA", "USDT");
            TransactionModel xrp = await Hitbtc.ShowCrypto("XRP", "USDT");

            List<string> prices = new List<string>();
            prices.Add(bitcoin.Price);
            prices.Add(etherium.Price);
            prices.Add(solana.Price);
            prices.Add(cardano.Price);
            prices.Add(xrp.Price);

            //Currency List
            List<SelectListItem> itemsFiat = new List<SelectListItem>();
            itemsFiat.Add(new SelectListItem { Text = "USD", Value = "0", Selected = true });
            itemsFiat.Add(new SelectListItem { Text = "CAD", Value = "1" });
            itemsFiat.Add(new SelectListItem { Text = "COP", Value = "2" });
            itemsFiat.Add(new SelectListItem { Text = "BRL", Value = "3" });
            ViewBag.FiatCurrencies = itemsFiat;

            //Crypto currency coins
            List<SelectListItem> itemsCrypto = new List<SelectListItem>();
            itemsCrypto.Add(new SelectListItem { Text = "BTC", Value = "0", Selected = true });
            itemsCrypto.Add(new SelectListItem { Text = "ETH", Value = "1" });
            itemsCrypto.Add(new SelectListItem { Text = "ADA", Value = "2" });
            itemsCrypto.Add(new SelectListItem { Text = "SOL", Value = "3" });
            itemsCrypto.Add(new SelectListItem { Text = "XRP", Value = "4" });

            ViewBag.CryptoCurrencies = itemsCrypto;

            ViewBag.btc = prices[0];
            ViewBag.eth = prices[1];
            ViewBag.sol = prices[2];
            ViewBag.ada = prices[3];
            ViewBag.xrp = prices[4];

            return View();
        }

    }
}
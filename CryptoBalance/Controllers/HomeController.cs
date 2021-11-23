using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Dashboard()
        {
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
            itemsCrypto.Add(new SelectListItem { Text = "CRO", Value = "4" });

            ViewBag.CryptoCurrencies = itemsCrypto;

            return View();
        }
    }
}
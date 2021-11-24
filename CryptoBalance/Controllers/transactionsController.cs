using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CryptoBalance;
using PagedList;

namespace CryptoBalance.Controllers
{
    public class transactionsController : Controller
    {
        private db_cryptoBalanceEntities1 db = new db_cryptoBalanceEntities1();

        // GET: transactions
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.currentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.MarketPriceParam = sortOrder == "Market Price" ? "marketPrice_desc" : "Market Price";
            ViewBag.AmountParam = sortOrder == "Amount" ? "amount_desc" : "Amount";
            ViewBag.TotalCoinsParam = sortOrder == "Total Coins" ? "totalCoins_desc" : "Total Coins";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            
            HttpCookie cookie = Request.Cookies["AuthCookie"];
            var getTransactions = from tr in db.transactions
                                    where tr.username == cookie.Value
                                    select tr;
            if (!String.IsNullOrEmpty(searchString))
            {
                getTransactions = getTransactions.Where(l => l.transaction_id.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    getTransactions = getTransactions.OrderByDescending(tr => tr.crypto_coin);
                    break;
                case "marketPrice_desc":
                    getTransactions = getTransactions.OrderByDescending(tr => tr.market_price);
                    break;
                case "Market Price":
                    getTransactions = getTransactions.OrderBy(tr => tr.market_price);
                    break;
                case "amount_desc":
                    getTransactions = getTransactions.OrderByDescending(tr => tr.amount);
                    break;
                case "Amount":
                    getTransactions = getTransactions.OrderBy(tr => tr.amount);
                    break;
                case "totalCoins_desc":
                    getTransactions = getTransactions.OrderByDescending(tr => tr.cryto_total);
                    break;
                case "Total Coins":
                    getTransactions = getTransactions.OrderBy(tr => tr.cryto_total);
                    break;
                default:
                    getTransactions = getTransactions.OrderBy(tr => tr.crypto_coin);
                    break;

            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //return View(logs.ToList());
            //getTransactions.ToPagedList(pageNumber, pageSize);
            //return RedirectToAction("Dashboard","Home", getTransactions.ToPagedList(pageNumber, pageSize));
            return View(getTransactions.ToPagedList(pageNumber, pageSize));
            //return View(db.logs.ToList());
        }

        //private ActionResult View(object p)
        //{
        //    throw new NotImplementedException();
        //}

        // GET: transactions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = db.transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: transactions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "transaction_id,crypto_coin,market_price,amount")] transaction transaction)
        {
            HttpCookie cookie = Request.Cookies["AuthCookie"];
            if (ModelState.IsValid)
            {
                transaction.username = cookie.Value;
                Double crypto_total = (Convert.ToDouble(transaction.amount) / Convert.ToDouble(transaction.market_price));
                transaction.cryto_total = Math.Round(crypto_total, 4);
                db.transactions.Add(transaction);
                db.SaveChanges();
                //return View(transaction);
                return RedirectToAction("Index", "transactions", transaction);
            }

            //return View(transaction);
            return RedirectToAction("Index", "transactions", transaction);
        }

        // GET: transactions/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = db.transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "username,transaction_id,crypto_coin,market_price,amount,cryto_total")] transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        // GET: transactions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            transaction transaction = db.transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            transaction transaction = db.transactions.Find(id);
            db.transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

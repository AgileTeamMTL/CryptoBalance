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
    public class usersController : Controller
    {
        private db_cryptoBalanceEntities1 db = new db_cryptoBalanceEntities1();

        // GET: users
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }

        // GET: users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "username,password,first_name,last_name,email,phone,city,province,country,date_of_birth,creation_date,modification_date")] user user)
        {
            user.creation_date = DateTime.Now;
            user.modification_date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return Redirect("/Home/Index");
                //return RedirectToAction("Index");
            }
            return Redirect("/Users/SignIn");
            //return View(user);
        }

        // GET: users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "username,password,first_name,last_name,email,phone,city,province,country,date_of_birth,creation_date,modification_date")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
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

        public ActionResult SignIn()
        {
            HttpCookie cookie = Request.Cookies["AuthCookie"];
            if (cookie == null)
            {
                return View();
            }
            else
            {
                return Redirect("Home/Dashboard");
            }
        }

        [HttpPost]
        public ActionResult SignIn(FormCollection fc)
        {
            //Username input was empty on the Form
            if (fc["Username"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(fc["Username"]);

            //Inputted username does not exist on the database
            if (user == null)
            {
                ViewBag.message = "User and password do not match";
                return View();
            }

            //User exists on the database
            else
            {
                //Check if the password matches for the selected user
                if (user.password.Equals(fc["password"].Trim()))
                {
                    //Authenticated
                    HttpCookie cookie = new HttpCookie("AuthCookie");
                    cookie.Value = fc["Username"];
                    ViewBag.salut = ("Hello " + fc["Username"] + ", welcome back!!");
                    cookie.Path = Request.ApplicationPath;
                    Response.Cookies.Add(cookie);

                    return Redirect("/transactions/Index");
                }
                else
                {
                    ViewBag.message = "User and password do not match";
                    return View();
                }

            }
        }
        public ActionResult Logout()
        {
            HttpCookie cookie = Request.Cookies["AuthCookie"];
            //If it doesnt exist, means the person is not logged in
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie.Path = Request.ApplicationPath;
                Response.Cookies.Add(cookie);
            }
            //Check here
            return Redirect("/users/SignIn");
        }
        
    }
}

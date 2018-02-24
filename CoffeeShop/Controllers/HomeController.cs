using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using CoffeeShop.Models;

namespace CoffeeShop.Controllers
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

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult SearchItemsByName(string SearchKey)
        {
            CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();
            ViewBag.ItemInfo = ORM.Items.Where(x => x.Name.Contains(SearchKey)).ToList();
            return View("Index");
        }

        public ActionResult AddUser(User newUser)
        {
            CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();
            bool validation = true;
            if(!Regex.IsMatch(newUser.FirstName, "^[a-zA-Z]{1,}$"))
            {
                ViewBag.InputError = "invalid input (letters only)";
                validation = false;
            }
            if(!Regex.IsMatch(newUser.LastName, "^[a - zA - Z]{ 1,}$"))
            {
                ViewBag.InputError = "invalid input (letters only)";
            }
            if (!Regex.IsMatch(newUser.Email, @"^(([^<> ()\[\]\\.,;:\s\@\""]+(\.[^<>()\[\]\\.,;:\s@\""]+)*)| (\""\.\+\""))\@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a - zA - Z\-0 - 9]+\.)+[a-zA-Z]{2,}))$"))
            {
                ViewBag.InputError = "Invalid input (try: email@domain.com)";
                validation = false;
            }
            if(!Regex.IsMatch(newUser.PhoneNo, @"^\d{10}$"))
            {
                ViewBag.InputError = "Invlid input (numbers only)";
                validation = false;
            }
            if(!Regex.IsMatch(newUser.Username, "^([a-zA-Z0-9]){1,15}$"))
            {
                ViewBag.InputError = "That username is not valid";
                validation = false;
            }
            if (!Regex.IsMatch(newUser.Password, @"^ (?=.*[a - z])(?=.*[A - Z])(?=.*\d)(?=.*[$@$!% *? &])[A - Za - z\d$@$!% *? &]{ 8,}"))
            {
                ViewBag.InputError = "Password isn't strong enough";
                validation = false;
            }

            if (validation == false)
                return View("Register");
            else
            {
                ORM.Users.Add(newUser);
                ORM.SaveChanges();
                ViewBag.RegisterNewUser = $"Hello, {newUser.FirstName}!";
                ViewBag.InputError = "";
                return View();
            }
        }
    }
}
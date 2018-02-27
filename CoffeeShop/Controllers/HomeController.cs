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
            try
            {
                CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();
                ViewBag.ItemInfo = ORM.Items.Where(x => x.Name.Contains(SearchKey)).ToList();
                return View("Index");
            }
            catch(Exception)
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult AddUser(User newUser)
        {
            try
            {
                CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();
                bool validation = true;
                if (newUser.FirstName == null)
                {
                    ViewBag.FirstNameError = "Invalid first name (letters only)";
                    validation = false;
                }
                if (newUser.LastName == null)
                {
                    ViewBag.LastNameError = "Invalid last name (letters only)";
                }
                if (newUser.Email == null)
                {
                    ViewBag.EmailError = "Invalid email (try: email@domain.com)";
                    validation = false;
                }
                if (newUser.PhoneNo == null)
                {
                    ViewBag.PhoneError = "Invalid phone number (numbers only)";
                    validation = false;
                }
                if (newUser.Username == null)
                {
                    ViewBag.UsernameError = "Invalid username";
                    validation = false;
                }
                if (newUser.Password == null)
                {
                    ViewBag.PasswordError = "Password isn't strong enough";
                    validation = false;
                }

                if (validation == false)
                    return View("Register");
                else
                {
                    ORM.Users.Add(newUser);
                    ORM.SaveChanges();
                    ViewBag.RegisterNewUser = $"Hello, {newUser.FirstName}!";
                    ViewBag.FirstNameError = "";
                    ViewBag.LastNameError = "";
                    ViewBag.EmailError = "";
                    ViewBag.PhoneError = "";
                    ViewBag.UsernameError = "";
                    ViewBag.PasswordError = "";

                    return View();
                }
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult Admin()
        {
            try
            {
                CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();
                ViewBag.ItemList = ORM.Items.ToList();
                return View();
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult AddItem()
        {
            return View();
        }

        public ActionResult SaveNewItem(Item newItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("../Shared/Error");
                }
                CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();
                ORM.Items.Add(newItem);
                ORM.SaveChanges();
                return RedirectToAction("Admin");
            }
            catch(Exception)
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult EditItem(string Name)
        {
            try
            {
                CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();
                Item ToEdit = ORM.Items.Find(Name);
                ViewBag.ItemToEdit = ToEdit;
                return View();
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult SaveEdit(Item newItem)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("../Shared/Error");
                }
                CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();
                ORM.Entry(ORM.Items.Find(newItem.Name)).CurrentValues.SetValues(newItem);
                ORM.SaveChanges();
                return RedirectToAction("Admin");
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult DeleteItem(string Name)
        {
            try
            {
                CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();
                ORM.Items.Remove(ORM.Items.Find(Name));
                ORM.SaveChanges();
                return RedirectToAction("Admin");
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }
    }
}
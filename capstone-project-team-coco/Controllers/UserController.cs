using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using we_watch.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace we_watch.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {

            // For Testing purposes - remove start
            HttpContext.Session.SetString("isLoggedIn", "false");
            HttpContext.Session.SetInt32("User", 1);
            // end remove

            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-3.1
            // ASP.NET 3.1 set up instructions for creating a session state
            // Used to pass information of whether a user is logged in or not and their userID.

            if (HttpContext.Session.GetString("isLoggedIn") == null)
            { HttpContext.Session.SetString("isLoggedIn", "false"); }
            else if (HttpContext.Session.GetString("isLoggedIn") == "true")
            {
                return RedirectToAction("Index", "ShowCard");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (email == null && password == null)
            {
                ViewBag.erroremailpassword = "Please enter an email address & password.";
            }
            else if (email == null)
            {
                ViewBag.erroremail = "Please enter an email address.";
            }
            else if (password == null)
            {
                ViewBag.errorpassword = "Please enter a password.";
            }
            if (ModelState.IsValid)
            {
                if (email == null)
                {
                    ViewBag.email = "Please enter an email address.";
                }
                else
                {
                    using (WeWatchContext context = new WeWatchContext())
                    {


                        // checking the inputted email against what's in our db
                        User potentialUser = context.User.Where(x => x.Email == email).SingleOrDefault();
                        // grab the salt value from that specific user
                        if (potentialUser != null)
                        {
                            string Salt = potentialUser.Salt;

                            // we need to check the password that they have inputted + salt value matches what's in their hashpassword in the db
                            if (Hash(password + Salt) == potentialUser.HashPassword)
                            {
                                HttpContext.Session.SetString("isLoggedIn", "true");
                                HttpContext.Session.SetInt32("User", potentialUser.UserID);
                                return RedirectToAction("Index", "ShowCard");
                            }

                            ViewBag.errorwronglogin = "The e-mail and/or password entered is incorrect. Please try again.";
                            ViewBag.Email = email;
                            ViewBag.HashPassword = password;

                            return View(); // change to show page


                        }
                    }
                }
            }
                // default view
                return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("User");
            HttpContext.Session.Remove("isLoggedIn");
            return Redirect("Login");
        }
            public IActionResult SignUp()
        {
            // needed to show our form
            return View();
        }

        [HttpPost] //method runs on submit

        public IActionResult SignUp(string email, string confirmedemail, string password, string confirmedpassword)
        {
            if (validateEmailPassword(email, password, confirmedemail, confirmedpassword))
            {

                using (WeWatchContext context = new WeWatchContext())
                {
                    if (context.User.Where(x => x.Email.ToUpper() == email.Trim().ToUpper()).Count() > 0)
                    {
                        ViewBag.usedemail = "This email has already been used. Please log in.";
                        return View();
                    }

                    else
                    {
                        Random r = new Random();
                        string Salt = Convert.ToString(r.Next());

                        User newUser = new User() { Email = email, HashPassword = Hash(password + Salt), Salt = Salt };

                        context.User.Add(newUser);

                        context.SaveChanges();
                        return Redirect("Login");

                    }
                }
            }

            else
            {
                return View();
            }
        }

        // method to validate email & password
        bool validateEmailPassword(string email, string password, string confirmedemail, string confirmedpassword)

        {
            bool isValid = true;

            if (email == null)
            {
                isValid = false;
                ViewBag.erroremail = "Please enter an email address.";
            }

            else if (confirmedemail == null)
            {
                isValid = false;
                ViewBag.errorconfirmedemail = "Please confirm your email.";
            }

            else if (email != confirmedemail)
            {
                isValid = false;
                ViewBag.errormatchingemail = "These emails do not match. Please try again.";
            }

            if (password == null)
            {
                isValid = false;
                ViewBag.errorpassword = "Please enter a password.";
            }

            else if (password.Length < 8 || !password.Any(char.IsUpper) || !password.Any(char.IsDigit))
            {
                isValid = false;
                ViewBag.errorpasswordconstraint = "Please choose a password with at least 8 characters, one capital letter, and one digit.";
            }

            else if (confirmedpassword == null)
            {
                isValid = false;
                ViewBag.errorconfirmedpassword = "Please confirm your password.";
            }

            else if (password != confirmedpassword)
            {
                isValid = false;
                ViewBag.errormatchingpassword = "These passwords do not match. Please try again.";
            }

            return isValid;
        }

        public static string Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }

    }

}
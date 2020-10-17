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


namespace we_watch.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            using (WeWatchContext context = new WeWatchContext())
            {
                int count = context.User.Count();
                ViewBag.Count = count;

            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
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
                        if (Authenticate.Hash(password + Salt) == potentialUser.HashPassword)
                        {
                            return RedirectToAction("Index", "ShowCard");
                        }
                        {
                            ViewBag.Email = "The e-mail and/or password entered is incorrect. Please try again.";
                        }
                        ViewBag.Email = email;
                        ViewBag.HashPassword = password;

                        return View(); // change to show page


                    }
                }
            }
            if (email == null)
            {
                ViewBag.email = "Please enter an email address.";
            }

            // default view
            return View();
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

                        User newUser = new User() { Email = email, HashPassword = Authenticate.Hash(password + Salt), Salt = Salt };

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
                ViewBag.email = "Please enter an email address.";
            }

            else if (confirmedemail == null)
            {
                isValid = false;
                ViewBag.confirmedemail = "Please confirm your email.";
            }

            else if (email != confirmedemail)
            {
                isValid = false;
                ViewBag.matchingemail = "These emails do not match. Please try again.";
            }

            if (password == null)
            {
                isValid = false;
                ViewBag.password = "Please enter a password.";
            }

            else if (password.Length < 8 || !password.Any(char.IsUpper) || !password.Any(char.IsDigit))
            {
                isValid = false;
                ViewBag.passworderror = "Please choose a password with at least 8 characters, one capital letter, and one digit.";
            }

            else if (confirmedpassword == null)
            {
                isValid = false;
                ViewBag.confirmedemail = "Please confirm your password.";
            }

            else if (password != confirmedpassword)
            {
                isValid = false;
                ViewBag.confirmedpassword = "These passwords do not match. Please try again.";
            }

            return isValid;
        }

    }

}
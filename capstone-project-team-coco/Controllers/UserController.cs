using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using we_watch.Models;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace we_watch.Controllers
{
    public class UserController : Controller
    {
        // Sets the session, if the user if logged in redirects them to the ShowCards Page
        public IActionResult Login()
        {

            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-3.1
            // ASP.NET 3.1 set up instructions for creating a session state
            // Used to pass information of whether a user is logged in or not and their userID.

            if (HttpContext.Session.GetString("isLoggedIn") == null)
            { HttpContext.Session.SetString("isLoggedIn", "false"); }
/*            else if (HttpContext.Session.GetString("isLoggedIn") == "true")
            {
                return RedirectToAction("Shows", "ShowCard");
            }*/
            return View();
        }
        [HttpPost]

        // Verifies wether the users name and password match what is stored in the Db
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
                    using WeWatchContext context = new WeWatchContext();


                    // checking the inputted email against what's in our db
                    User potentialUser = context.User.Where(x => x.Email == email.Trim().ToLower()).SingleOrDefault();
                    // grab the salt value from that specific user
                    if (potentialUser != null)
                    {
                        string Salt = potentialUser.Salt;

                        // we need to check the password that they have inputted + salt value matches what's in their hashpassword in the db
                        if (Hash(password + Salt) == potentialUser.HashPassword)
                        {
                            HttpContext.Session.SetString("isLoggedIn", "true");
                            HttpContext.Session.SetInt32("User", potentialUser.UserID);
                            return RedirectToAction("Shows", "ShowCard");
                        }

                        ViewBag.errorwronglogin = "The e-mail and/or password entered is incorrect. Please try again.";

                        return View(); // change to show page


                    }
                }
            }
                // default view
                return View();
        }

        // Logs the user out by clearing the session data
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("User");
            HttpContext.Session.Remove("isLoggedIn");
            return Redirect("Login");
        }

        // Displays the Signup form
            public IActionResult SignUp()
        {
            // needed to show our form
            return View();
        }

        [HttpPost] //method runs on submit to add user to the Db

        public IActionResult SignUp(string email, string confirmedemail, string password, string confirmedpassword)
        {
            if (ValidateEmailPassword(email, password, confirmedemail, confirmedpassword))
            {

                using WeWatchContext context = new WeWatchContext();
                if (context.User.Where(x => x.Email.Trim().ToLower() == email.Trim().ToLower()).Count() > 0)
                {
                    ViewBag.usedemail = "This email has already been used. Please log in.";
                    return View();
                }

                else
                {

                    // Creates a random salt value
                    Random r = new Random();
                    string Salt = Convert.ToString(r.Next());

                    // Adds the salt value to the password and feeds that to the Hash method
                    User newUser = new User() { Email = email.Trim().ToLower(), HashPassword = Hash(password + Salt), Salt = Salt };

                    context.User.Add(newUser);

                    context.SaveChanges();
                    return Redirect("Login");

                }
            }

            else
            {
                return View();
            }
        }

        // method to validate email & password
        bool ValidateEmailPassword(string email, string password, string confirmedemail, string confirmedpassword)

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

            else if (email.Trim().ToLower() != confirmedemail.Trim().ToLower())
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

            // Citation
            //  https://www.youtube.com/watch?v=gSJFjuWFTdA&list=PLgX_X6wpWU2RZ12GQnTBhVH3DmuTiFwm_&index=1&t=3171s&ab_channel=souravmondal
            // SHA256 is a hashing algorithm used to secure passwords
            // The build in Crytography includes this method
           
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }

    }

}
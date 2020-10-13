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
                    if (context.User.Where(x => x.Email == email).Count() != 1)
                    {
                        ViewBag.Email = "An incorrect Email and/or password";
                        ViewBag.Password = "Please Try Again";
                    }
                    else
                    {
                        ViewBag.Email = email;
                        ViewBag.Password = password;

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

/*            using (WeWatchContext context = new WeWatchContext())
            {
                if (context.User.Where(x => x.Email.ToUpper() == email.Trim().ToUpper()).Count() > 0)
                {
                    ViewBag.usedemail = "This email has already been used. Please log in.";
                }

                else
                {
                    User newUser = new User() { Email = email };

                    context.User.Add(newUser);
                    context.SaveChanges();
                    ViewBag.signupsuccess = $"Welcome to WeWatch! You can now " <a href="">log in!</a>";

                }
            }*/

            if (email == null)
            {
                ViewBag.email = "Please enter an email address.";
            }

            else if (confirmedemail == null)
            {
                ViewBag.confirmedemail = "Please confirm your email.";
            }

            else if (email != confirmedemail)
            {
                ViewBag.matchingemail = "These emails do not match. Please try again.";
            }

            if (password.Length < 8 && password.All(char.IsLower) && password.All(char.IsLetter))
            {
                ViewBag.passworderror = "Please choose a password with at least 8 characters, one capital letter, and one digit.";
            }

            else if (password == null)
            {
                ViewBag.password = "Please enter a password.";
            }

            else if (confirmedpassword == null)
            {
                ViewBag.confirmedemail = "Please confirm your password.";
            }
          
            else if (password != confirmedpassword)
            {
                ViewBag.confirmedpassword = "These passwords do not match. Please try again.";
            }

            return View();


        }
        /* //source: https://www.codeproject.com/Tips/222203/Customizable-password-Policy-Csharp
         public class PasswordPolicy
         {
             private static int Minimum_Length = 7;
             private static int Upper_Case_length = 1;
             private static int Lower_Case_length = 1;
             private static int NonAlpha_length = 1;
             private static int Numeric_length = 1;

             public static bool IsValid(string password)
             {
                 if (password.Length < Minimum_Length)
                     return false;
                 if (UpperCaseCount(password) < Upper_Case_length)
                     return false;
                 if (LowerCaseCount(password) < Lower_Case_length)
                     return false;
                 if (NumericCount(password) < 1)
                     return false;
                 if (NonAlphaCount(password) < NonAlpha_length)
                     return false;
                 return true;
             }

             private static int UpperCaseCount(string password)
             {
                 return Regex.Matches(password, "[A-Z]").Count;
             }

             private static int LowerCaseCount(string password)
             {
                 return Regex.Matches(password, "[a-z]").Count;
             }
             private static int NumericCount(string password)
             {
                 return Regex.Matches(password, "[0-9]").Count;
             }
             private static int NonAlphaCount(string password)
             {
                 return Regex.Matches(password, @"[^0-9a-zA-Z\._]").Count;
             }
         }
 */

        /* if (ViewBag.email != ViewBag.confirmedemail)
         {



             using (WeWatchContext context = new WeWatchContext())
             {
                 if (context.User.Where(x => x.Email == email).Count() != 1)
                 {
                     ViewBag.Email = "An incorrect Email and/or password";
                     ViewBag.password = "Please Try Again";
                 }
                 else
                 {
                     ViewBag.Email = email;
                     ViewBag.password = password;

                 }

    }
        }


        // default view
        return View();
       */



    }


}






// GET: Shows/Create
/*public IActionResult Create()
{
    ViewData["UserID"] = new SelectList(_context.User, "UserID", "Email");
    return View();
}

// POST: Shows/Create
// To protect from overposting attacks, enable the specific properties you want to bind to, for 
// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("ShowID,UserID,Title,TotalSeasons")] Show show)
{
    if (ModelState.IsValid)
    {
        _context.Add(show);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    ViewData["UserID"] = new SelectList(_context.User, "UserID", "Email", show.UserID);
    return View(show);
}*/

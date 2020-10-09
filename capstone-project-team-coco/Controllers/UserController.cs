using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using we_watch.Models;

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
                        ViewBag.Email = "An incorrect Email and/or Password";
                        ViewBag.Password = "Please Try Again";
                    }
                    else
                    {
                        ViewBag.Email = email;
                        ViewBag.Password = password;

                    }

                }
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

            else if(password == null)
            {
                ViewBag.password = "Please enter a password.";
            }

            else if(confirmedpassword == null)
            {
                ViewBag.confirmedemail = "Please confirm your password.";
            }

            else if (password != confirmedpassword)
            {
                ViewBag.confirmedpassword = "These passwords do not match. Please try again.";
            }

            return View();
        }
            


            /* if (ViewBag.email != ViewBag.confirmedemail)
             {



                 using (WeWatchContext context = new WeWatchContext())
                 {
                     if (context.User.Where(x => x.Email == email).Count() != 1)
                     {
                         ViewBag.Email = "An incorrect Email and/or Password";
                         ViewBag.Password = "Please Try Again";
                     }
                     else
                     {
                         ViewBag.Email = email;
                         ViewBag.Password = password;

                     }*/

        //}
           // }


            // default view
           // return View();
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

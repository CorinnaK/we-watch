using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using we_watch.Models;

namespace we_watch.Controllers
{
    public class WatcherController : Controller
    {
        public IActionResult AddWatcher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddWatcher(string name)
        {

            using (WeWatchContext context = new WeWatchContext())
            {
                if (name == null)
                {
                    ViewBag.nameError = "Please enter a name for the Watcher you want to add.";
                }

                else if (context.Watcher.Where(x => x.Name.ToUpper() == name.Trim().ToUpper()).Count() > 0)
                {
                    ViewBag.nameError = "You already have a Watcher with that name.";
                }

                else
                { 
                Watcher newWatcher = new Watcher() { Name = name};

                    context.Watcher.Add(newWatcher);
                    context.SaveChanges();
                    ViewBag.Success = $"Successfully added {name} to your Watcher list.";

                }
            }
               return View();
        }
    }
}

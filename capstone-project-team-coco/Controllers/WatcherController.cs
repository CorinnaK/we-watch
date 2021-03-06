﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using we_watch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace we_watch.Controllers
{
    public class WatcherController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("ManageWatchers");
        }


        // Central view for CRUD actions on Watcher 
        public IActionResult ManageWatchers()
        {
            // Ensure user is logged in
            if (!IsLoggedIn()) { return RedirectToAction("Login", "User"); }

            List<Watcher> watchers;
            using (WeWatchContext context = new WeWatchContext())
            {
                //List of all watchers in the Db
                watchers = context.Watcher.OrderBy(x=>x.Name).ToList();
            }

            ViewBag.AllWatchers = watchers;
            ViewBag.Message = TempData["message"]?.ToString();
            return View();
        }


        [HttpPost]
        // Adds a watcher to the Db
        public IActionResult AddWatcher(string name)
        {
            // Ensure user is logged in
            if (!IsLoggedIn()) { return RedirectToAction("Login", "User"); }

            string message = null;
            using (WeWatchContext context = new WeWatchContext())
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    message = "Please enter a name for the Watcher you want to add.";
                }
                else if (name.Trim().Length > 50)
                {
                    message = "Name cannot be more than 50 characters";
                }
                else if (context.Watcher.Where(x => x.Name.ToUpper() == name.Trim().ToUpper()).Count() > 0)
                {
                    message = $"You already have {name} is your list";
                }

                else
                {
                    Watcher newWatcher = new Watcher() { Name = name.Trim() };

                    context.Watcher.Add(newWatcher);
                    context.SaveChanges();
                    message = $"Successfully added {name} to your Watcher list.";

                }
            }
            TempData["message"] = message;
            return RedirectToAction("ManageWatchers");
        }


        [HttpPost]
        // Edits a watcher in the Db
        public IActionResult EditWatcher(string watcherName, string watcherID)
        {
            // Ensure user is logged in
            if (!IsLoggedIn()) { return RedirectToAction("Login", "User"); }

            string message = null;
            using (WeWatchContext context = new WeWatchContext())
            {
                string tempName;
                int parsedWatcherID = int.Parse(watcherID);

                // Search for the watcher
                Watcher watcher = context.Watcher.Where(x => x.WatcherID == parsedWatcherID).SingleOrDefault();
                
                tempName = watcher.Name;

                // Validate data
                if (watcher != null)
                {

                    if (string.IsNullOrWhiteSpace(watcherName))
                    {
                        message = "Name must contain characters";
                    }
                    else if (watcherName.Trim().Length > 50)
                    {
                        message = "Name cannot be more than 50 characters";
                    }
                    else if (watcher.Name.ToUpper() == watcherName.Trim().ToUpper())
                    {
                        message = "No changes were detected";
                    }
                    else if (context.Watcher.Where(x => x.Name.ToUpper() == watcherName.Trim().ToUpper()).Count() != 0)
                    {
                        message = $"{watcherName} already exists";
                    }
                    // Save to Db
                    else
                    {
                        watcher.Name = watcherName;
                        context.SaveChanges();
                        message = $"Successfully updated {tempName} to {watcher.Name} ";
                    }
                }

                else
                {
                    message = "Cannot find watcher. Please refresh and try again.";

                }
            }

            TempData["message"] = message;
            return RedirectToAction("ManageWatchers");
        }


        // Deletes a watcher from the Db
        public IActionResult DeleteWatcher(int watcherID)
        {
            if (!IsLoggedIn()) { return RedirectToAction("Login", "User"); }

            string message = null;
            if (watcherID == 0)
            { message = "No program was provided."; }
            else
            {
                using WeWatchContext context = new WeWatchContext();

                Watcher watcher = context.Watcher.Where(x => x.WatcherID == watcherID).SingleOrDefault();
                
                if (watcher == null)
                { message = "Cannot delete watcher. Please try again later."; }
                
                else
                {
                    string tempName = watcher.Name;
                    context.Watcher.Remove(watcher);
                    context.SaveChanges();
                    message = $"Successfully deleted {tempName}";
                }
            }

            TempData["message"] = message;
            return RedirectToAction("ManageWatchers");
        }

        // Checks if the user is logged in
        public bool IsLoggedIn()
        {
            return (HttpContext.Session.GetString("isLoggedIn") == "true");
        }
    }
}


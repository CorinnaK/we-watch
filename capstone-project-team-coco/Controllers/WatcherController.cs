using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using we_watch.Models;
using Microsoft.AspNetCore.Http;

namespace we_watch.Controllers
{
    public class WatcherController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("ManageWatchers");
        }
        public IActionResult ManageWatchers()
        {

            List<Watcher> watchers;
            using (WeWatchContext context = new WeWatchContext())
            {
                watchers = context.Watcher.OrderBy(x=>x.Name).ToList();
            }
            ViewBag.AllWatchers = watchers;
            ViewBag.messages = TempData["message"]?.ToString();
            return View();
        }

        [HttpPost]
        public IActionResult AddWatcher(string name)
        {

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

        public IActionResult EditWatcher(string watcherName, string watcherID)
        {
            string message = null;
            using (WeWatchContext context = new WeWatchContext())
            {
                string tempName;
                int parsedWatcherID = int.Parse(watcherID);
                Watcher watcher = context.Watcher.Where(x => x.WatcherID == parsedWatcherID).SingleOrDefault();
                tempName = watcher.Name;
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
        public IActionResult DeleteWatcher(int watcherID)
        {
            if (!isLoggedIn()) { return RedirectToAction("Login", "User"); }

            string message = null;
            if (watcherID == 0)
            { message = "No program was provided."; }
            else
            {
                using (WeWatchContext context = new WeWatchContext())
                {
                    string tempName = context.Watcher.Where(x => x.WatcherID == watcherID).Select(y => y.Name).Single().ToString();


                    Watcher watcher = context.Watcher.Where(x => x.WatcherID == watcherID).SingleOrDefault();
                    if (watcher == null)
                    { message = "Cannot delete watcher. Please try again later."; }
                    else
                    {
                        context.Watcher.Remove(watcher);
                        context.SaveChanges();
                        message = $"Successfully deleted {tempName}";
                    }
                }
            }

            TempData["message"] = message;
            return RedirectToAction("ManageWatchers");
        }

        public bool isLoggedIn()
        {
            return (HttpContext.Session.GetString("isLoggedIn") == "true");
        }
    }
}


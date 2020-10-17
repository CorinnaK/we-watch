using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using we_watch.Models;
using Microsoft.AspNetCore.Http;

namespace we_watch.Controllers
{
    public class WatcherController : Controller
    {
        public IActionResult Index()
        {
            List<Watcher> watchers;
            using (WeWatchContext context = new WeWatchContext())
            {
                watchers = context.Watcher.ToList();
            }

            ViewBag.AllWatchers = watchers;
            return View();
        }
        public IActionResult ManageWatcher(string message)
        {
            
            List<Watcher> watchers;
            using (WeWatchContext context = new WeWatchContext())
            {
                watchers = context.Watcher.ToList();
            }
            ViewBag.AllWatchers = watchers;
            ViewBag.Message = message;
   

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
                else if (name.Length > 50)
                {
                    message = "Name cannot be more than 50 characters";
                }
                else if (context.Watcher.Where(x => x.Name.ToUpper() == name.Trim().ToUpper()).Count() > 0)
                {
                    message = "You already have a Watcher with that name.";
                }

                else
                {
                    Watcher newWatcher = new Watcher() { Name = name.Trim() };

                    context.Watcher.Add(newWatcher);
                    context.SaveChanges();
                    message = $"Successfully added {name} to your Watcher list.";

                }
            }

            return RedirectToAction("ManageWatcher", new { message});
        }
        [HttpPost]

        public IActionResult EditWatcher(string watcherName, string watcherID, string editWatcher, string deleteWatcher)
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
                    if (deleteWatcher != null)
                    {
                        context.Watcher.Remove(watcher);
                        context.SaveChanges();
                        message = $"You have removed {tempName} from your list.";

                    }
                    else if (editWatcher != null)
                    {
                        if (string.IsNullOrWhiteSpace(watcherName))
                        {
                            message = "Name must contain characters";
                        }
                        else if (watcherName.Length > 50)
                        {
                            message = "Name cannot be more than 50 characters";
                        }
                        else if (watcher.Name == watcherName)
                        {
                            message = "No changes were detected";
                        }
                        else if (context.Watcher.Where(x => x.Name == watcherName).Count() != 0)
                        {
                            message = "That watcher already exists";
                        }
                        else
                        {
                            watcher.Name = watcherName;
                            context.SaveChanges();
                            message = $"Successfully updated {tempName} to {watcher.Name} ";
                        }
                    }
                }
                else
                {
                    message = "Cannot find watcher. Please refresh and try again.";

                }
            }
            return RedirectToAction("ManageWatcher", new { message });
        }
    }
}

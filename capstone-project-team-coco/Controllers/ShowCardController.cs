using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using we_watch.Models;
namespace we_watch.Controllers
{
    public class ShowCardController : Controller
    {

        public IActionResult Index()
        {
            int userID = GetUserID();

            // If the userId was not found(0) redirect to the login page
            if (userID == 0)
            { return RedirectToAction("Login", "User"); }

            List<ShowCard> allShows = new List<ShowCard>();
            using (WeWatchContext context = new WeWatchContext())
            {

                allShows = context.ShowCard.Include("Show").Include("Watcher").Where(x => x.UserID == userID).Distinct().OrderBy(x => x.Show.Title).ToList();

            }
            ViewBag.AllShows = allShows;
            return View();
        }


        public IActionResult CreateCard(int showID, int watcherID, int seasonID, int episode)
        {
            string message = TempData["message"]?.ToString();
            int userID = GetUserID();
            if (userID == 0)
            { return RedirectToAction("Login", "User"); }

            using (WeWatchContext context = new WeWatchContext())
            {

                Show selectedShow = new Show();
                ShowSeason selectedSeason = new ShowSeason();
                Watcher selectedWatcher = new Watcher();
                List<ShowSeason> seasons = new List<ShowSeason>();
              


                // A show and a watcher must be selected before seasons can be determined
                if (showID != 0 && watcherID != 0)
                {
                    selectedShow = context.Show.Where(x => x.ShowID == showID).Single();
                    selectedWatcher = context.Watcher.Where(x => x.WatcherID == watcherID).Single();

                    // Check to see if this user already has a card with this watcher and this show
                    if (context.ShowCard.Where(x => x.ShowID == selectedShow.ShowID && x.WatcherID == selectedWatcher.WatcherID && x.UserID == userID).Count() != 0)
                    {
                        message = $"You are already watching {selectedShow.Title} with {selectedWatcher.Name}";
                        selectedShow = null;
                        selectedWatcher = null;
                    }

                    // Populate the seasons for this show
                    else
                    {
                        seasons = context.ShowSeason.Where(x => x.ShowID == showID).ToList();

                        // Populate episodes if season has been selected
                        if (seasonID != 0)
                        {
                            selectedSeason = context.ShowSeason.Where(x => x.ShowSeasonID == seasonID).Single();
                        }
                    }
                }

                List<Show> allShows = context.Show.OrderBy(x => x.Title).ToList();
                List<Watcher> allWatchers = context.Watcher.OrderBy(x => x.Name).ToList();

                ViewBag.Message = message;
                ViewBag.SelectedShow = selectedShow;
                ViewBag.SelectedWatcher = selectedWatcher;
                ViewBag.AllShows = allShows;
                ViewBag.AllWatchers = allWatchers;
                ViewBag.Seasons = seasons;
                ViewBag.SelectedSeason = selectedSeason;
                ViewBag.Episode = episode;

                return View();
            }
        }

        public IActionResult Connect(int showID, int watcherID, int seasonID, int episode)
        {
            int userID = GetUserID();
            if (userID == 0)
            { return RedirectToAction("Login", "User"); }
            string message = "";

            if (showID == 0 || watcherID == 0 || seasonID == 0 || episode == 0)
            {
                return RedirectToAction("CreateCard");
            }


            using (WeWatchContext context = new WeWatchContext())
            {

                // Check to see if this user already has a card with this watcher and this show
                if (context.ShowCard.Where(x => x.ShowID == showID && x.UserID == userID && x.WatcherID == watcherID).Count() > 0)
                {
                    return Redirect("CreateCard");
                }

                ShowCard newShowCard = new ShowCard()
                {
                    ShowID = showID,
                    CurrentSeason = seasonID,
                    CurrentEpisode = episode,
                    UserID = userID,
                    WatcherID = watcherID,
                    Status = "Current"
                };
                try
                {
                    context.ShowCard.Add(newShowCard);
                    context.SaveChanges();
                    ViewBag.NewShowCard = newShowCard;
                    message = "Success";
                }
                catch
                {
                    message = "Something went wrong. Your connection was not saved. Please refresh and try again.";
                }

                TempData["showCardID"] = (int)context.ShowCard.Where(x => x.ShowID == showID && x.UserID == userID && x.WatcherID == watcherID).Select(x => x.ShowCardID).Single();
                TempData["message"] = message;
            }
            return Redirect("CreateCard");
        }
        public IActionResult EditCard()
        {
            int userID = GetUserID();
            if (userID == 0)
            { return RedirectToAction("Login", "User"); }

            int showCardID = int.Parse(TempData["showCardID"].ToString());
            ShowCard targetShowCard;
            using (WeWatchContext context = new WeWatchContext())
            {
                targetShowCard = context.ShowCard.Include("Show").Include("ShowSeason").Where(x => x.ShowCardID == showCardID).SingleOrDefault();
            }

            ViewBag.ShowCard = targetShowCard;
            return View();

        }
        // Method to check whether the user is logged in and if so, return the user ID.
        public int GetUserID()
        {

            int userID = 0;

            // Check if there is a isLoggedIn session cookie and it is set to true
            // Only set to true if the user successfully went through the login page
            if (HttpContext.Session.GetString("isLoggedIn") == "true")
            {
                // If they are logged in, grab their User ID that is stored in the session
                userID = (int)HttpContext.Session.GetInt32("User");
            }

            return userID;
        }
    }
}

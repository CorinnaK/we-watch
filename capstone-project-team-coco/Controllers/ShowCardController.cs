using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            List<Show> allShowCardShows = new List<Show>();
            using (WeWatchContext context = new WeWatchContext())
            {

                allShowCardShows = context.ShowCard.Include(x=>x.Show).Where(x => x.UserID == userID).Select(x=> x.Show).Distinct().ToList();
     
            }
            ViewBag.AllShowCardShows= allShowCardShows;
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
                DisplayCard card = new DisplayCard();

                // A show and a watcher must be selected before seasons can be determined
                if (showID != 0 && watcherID != 0)
                {
                    card = new DisplayCard(showID, watcherID);
     
                    // Check to see if this user already has a card with this watcher and this show
                    if (context.ShowCard.Where(x => x.ShowID == card.ShowID && x.WatcherID == card.WatcherID && x.UserID == userID).Count() != 0)
                    {
                        message = $"You are already watching {card.ShowTitle} with {card.WatcherName}";
                    }

                    // Populate the seasons for this show
                    else
                    {
                        seasons = context.ShowSeason.Where(x => x.ShowID == showID).ToList();

                        // Populate episodes if season has been selected
                        if (seasonID != 0)
                        {
                            card.SeasonID = seasonID;
                            selectedSeason = context.ShowSeason.Where(x => x.ShowSeasonID == seasonID).Single();
                            card.CurrentEpisode = episode;
                        }
                    }
                }
                if (TempData["showCardID"] != null)
                {
                    int showCardID = int.Parse(TempData["showCardID"].ToString());
                    ShowCard show = context.ShowCard.Where(x => x.ShowCardID == showCardID).Single();
                    card = new DisplayCard(show.ShowID, show.WatcherID);
                    card.SeasonID = show.CurrentSeason;
                    card.CurrentEpisode = show.CurrentEpisode;

                }
                ViewBag.Card = card;
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

            if (showID== 0 || watcherID == 0 || seasonID == 0 || episode == 0)
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
                    WatcherID = watcherID,
                    CurrentSeason = seasonID,
                    CurrentEpisode = episode,
                    UserID = userID,
                    Status = "Current"
                };
                try
                {
                    context.ShowCard.Add(newShowCard);
                    context.SaveChanges();
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
        public IActionResult ByShow(int showID)
        {
            int userID = GetUserID();
            if (userID == 0)
            { return RedirectToAction("Login", "User"); }
             
            List<DisplayCard> displayCards = new List<DisplayCard>();
            List<Show> allUserShows = new List<Show>();


            using (WeWatchContext context = new WeWatchContext())
            {

                
                List<ShowCard> showCardsByShow = context.ShowCard.Where(x => x.ShowID == showID && x.UserID == userID).ToList();


                allUserShows = context.ShowCard.Include(x=>x.Show).Where(x => x.UserID == userID).Select(x=>x.Show).Distinct().ToList();

                if (showCardsByShow != null)
                {
                    foreach (ShowCard showCard in showCardsByShow)
                    {
                        DisplayCard card = new DisplayCard(showCard.ShowID, showCard.WatcherID);
                        card.SeasonID = showCard.CurrentSeason;
                        card.CurrentEpisode = showCard.CurrentEpisode;
                        displayCards.Add(card);
                    }
                }
            }
            ViewBag.AllUserShows = allUserShows;
            ViewBag.DisplayCards = displayCards;
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

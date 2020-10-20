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
            return Redirect("Shows");
        }
       

        public IActionResult Shows()
        {
            int userID = GetUserID();

            // If the userId was not found(0) redirect to the login page
            if (userID == 0)
            { return RedirectToAction("Login", "User"); }

            List<Show> allShowCardShows = new List<Show>();
            using (WeWatchContext context = new WeWatchContext())
            {
                allShowCardShows = context.ShowCard.Include(x => x.Show).Where(x => x.UserID == userID).Select(x => x.Show).Distinct().ToList();

            }

            ViewBag.AllShowCardShows = allShowCardShows;
            return View();
        }


        public IActionResult CreateCard(int showID, int watcherID, int seasonID, int episode)
        {
            string message = TempData["message"]?.ToString();
            int userID = GetUserID();
            if (userID == 0)
            { return RedirectToAction("Login", "User"); }

            using WeWatchContext context = new WeWatchContext();
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
                    seasons = context.ShowSeason.Where(x => x.ShowID == showID).OrderBy(x => x.IndividualSeason).ToList();

                    // Populate episodes if season has been selected
                    if (seasonID != 0)
                    {
                        card.SeasonID = seasonID;
                        selectedSeason = context.ShowSeason.Where(x => x.ShowSeasonID == seasonID).Single();
                        card.CurrentEpisode = episode;
                    }
                }
            }
            // If we were passed a ShowCardID  from another action, create a displayCard
            if (TempData["showCardID"] != null)
            {
                int showCardID = int.Parse(TempData["showCardID"].ToString());
                ShowCard show = context.ShowCard.Where(x => x.ShowCardID == showCardID).Single();
                card = new DisplayCard(show.ShowID, show.WatcherID)
                {
                    SeasonID = show.CurrentSeason,
                    CurrentEpisode = show.CurrentEpisode
                };

            }

            ViewBag.Card = card;
            List<Show> allShows = context.Show.OrderBy(x => x.Title).ToList();
            List<Watcher> allWatchers = context.Watcher.OrderBy(x => x.Name).ToList();

            ViewBag.Message = message;
            ViewBag.AllShows = allShows;
            ViewBag.AllWatchers = allWatchers;
            ViewBag.Seasons = seasons;

            return View();
        }


        public IActionResult Connect(int showID, int watcherID, int seasonID, int episode)
        {
            int userID = GetUserID();
            if (userID == 0)
            { return RedirectToAction("Login", "User"); }

            string message = "";

            // If we are missing any fields, redirect the user to the CreateCard Action
            if (showID == 0 || watcherID == 0 || seasonID == 0 || episode == 0)
            {
                return RedirectToAction("CreateCard", new { showID, watcherID, seasonID, episode});
            }

            using (WeWatchContext context = new WeWatchContext())
            {

                // Check to see if this user already has a card with this watcher and this show
                if (context.ShowCard.Where(x => x.ShowID == showID && x.UserID == userID && x.WatcherID == watcherID).Count() > 0)
                {
                    message = "You must fill in all fields before you can Watch";
                    return Redirect("CreateCard");
                }

                // Create the card with 
                ShowCard newShowCard = new ShowCard()
                {
                    ShowID = showID,
                    WatcherID = watcherID,
                    CurrentSeason = seasonID,
                    CurrentEpisode = episode,
                    UserID = userID,
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
            string message = TempData["message"]?.ToString();
            int userID = GetUserID();
            if (userID == 0)
            { return RedirectToAction("Login", "User"); }

            List<DisplayCard> displayCards = new List<DisplayCard>();
            List<Show> allUserShows = new List<Show>();


            using (WeWatchContext context = new WeWatchContext())
            {


                List<ShowCard> showCardsByShow = context.ShowCard.Include(x => x.Show).Where(x => x.ShowID == showID && x.UserID == userID).ToList();


                allUserShows = context.ShowCard.Include(x => x.Show).Where(x => x.UserID == userID).Select(x => x.Show).Distinct().ToList();

                if (showCardsByShow != null)
                {
                    foreach (ShowCard showCard in showCardsByShow)
                    {
                        DisplayCard card = new DisplayCard(showCard.ShowID, showCard.WatcherID)
                        {
                            ShowCardID = showCard.ShowCardID,
                            SeasonID = showCard.CurrentSeason,
                            CurrentEpisode = showCard.CurrentEpisode
                        };
                        card.ShowCardID = showCard.ShowCardID;
                        displayCards.Add(card);
                    }
                }
            }
            ViewBag.Message = message;
            ViewBag.AllUserShows = allUserShows;
            ViewBag.DisplayCards = displayCards;
            return View();

        }


        public IActionResult Watchers()
        {
            int userID = GetUserID();

            // If the userId was not found(0) redirect to the login page
            if (userID == 0)
            { return RedirectToAction("Login", "User"); }

            List<Watcher> allShowCardWatchers = new List<Watcher>();
            using (WeWatchContext context = new WeWatchContext())
            {

                allShowCardWatchers = context.ShowCard.Include(x => x.Watcher).Where(x => x.UserID == userID).Select(x => x.Watcher).Distinct().ToList();

            }
            ViewBag.AllShowCardWatchers = allShowCardWatchers;
            return View();
        }


        public IActionResult ByWatcher(int watcherID)
        {
            string message = TempData["message"]?.ToString();
            int userID = GetUserID();
            if (userID == 0)
            { return RedirectToAction("Login", "User"); }

            List<DisplayCard> displayCards = new List<DisplayCard>();
            List<Watcher> allUserWatchers = new List<Watcher>();

            using (WeWatchContext context = new WeWatchContext())
            {
                if (watcherID == 0)
                {
                    message = "Unable to find that Watcher.";
                }
                else
                {
                    List<ShowCard> showCardsByWatcher = context.ShowCard.Include(x => x.Watcher).Where(x => x.WatcherID == watcherID && x.UserID == userID).ToList();


                    allUserWatchers = context.ShowCard.Include(x => x.Watcher).Where(x => x.UserID == userID).Select(x => x.Watcher).Distinct().ToList();
                    if (showCardsByWatcher == null)
                    { message = "Looks like you are not watching a show with that Watcher"; }

                    if (showCardsByWatcher != null)
                    {
                        foreach (ShowCard showCard in showCardsByWatcher)
                        {
                            DisplayCard card = new DisplayCard(showCard.ShowID, showCard.WatcherID)
                            {
                                ShowCardID = showCard.ShowCardID,
                                SeasonID = showCard.CurrentSeason,
                                CurrentEpisode = showCard.CurrentEpisode
                            };
                            card.ShowCardID = showCard.ShowCardID;
                            displayCards.Add(card);
                        }
                    }
                }
            }
            ViewBag.Message = message;
            ViewBag.AllUserWatchers = allUserWatchers;
            ViewBag.DisplayCards = displayCards;
            return View();

        }


        public IActionResult AddEpisode(int showCardID)
        {
            int userID = GetUserID();
            if (userID == 0)
            { return RedirectToAction("Login", "User"); }

            string message = null;
            ShowCard show = new ShowCard();
            using (WeWatchContext context = new WeWatchContext())
            {
                show = context.ShowCard.Include(x => x.Show).Where(x => x.ShowCardID == showCardID).Single();
                ShowSeason currentSeason = context.ShowSeason.Where(x => x.ShowID == show.ShowID && x.ShowSeasonID == show.CurrentSeason).Single();
                if (show.CurrentEpisode >= currentSeason.SeasonEpisodes)
                {

                    ShowSeason nextSeason = context.ShowSeason.Include(x => x.Show).Where(x => x.ShowID == show.ShowID && x.IndividualSeason > currentSeason.IndividualSeason).OrderBy(x => x.IndividualSeason).FirstOrDefault();
                    if (nextSeason == null)
                    { message = "Sorry you have reached the end. Would you like to add another season?"; }
                    else
                    {
                        show.CurrentEpisode = 1;
                        show.CurrentSeason = nextSeason.ShowSeasonID;
                        message = "You started a new Season!";
                    }
                }
                else
                {
                    show.CurrentEpisode++;
                    message = "Next episode";
                }

                context.SaveChanges();
            }
            TempData["message"] = message;
            return RedirectToAction("ByShow", new { showID = show.ShowID });
        }


        public IActionResult MinusEpisode(int showCardID)
        {
            int userID = GetUserID();
            if (userID == 0)
            { return RedirectToAction("Login", "User"); }

            string message = null;
            ShowCard show = new ShowCard();
            using (WeWatchContext context = new WeWatchContext())
            {
                show = context.ShowCard.Include(x => x.Show).Where(x => x.ShowCardID == showCardID).Single();
                ShowSeason currentSeason = context.ShowSeason.Where(x => x.ShowID == show.ShowID && x.ShowSeasonID == show.CurrentSeason).Single();
                if (show.CurrentEpisode == 1)
                {

                    ShowSeason pastSeason = context.ShowSeason.Include(x => x.Show).Where(x => x.ShowID == show.ShowID && x.IndividualSeason < currentSeason.IndividualSeason).OrderBy(x => x.IndividualSeason).FirstOrDefault();
                    if (pastSeason == null)
                    { message = "Sorry you no season to go back to. Would you like to add a season to the list?"; }
                    else
                    {
                        show.CurrentEpisode = pastSeason.SeasonEpisodes;
                        show.CurrentSeason = pastSeason.ShowSeasonID;
                        message = "You went back a SEASON";
                    }
                }
                else
                {
                    show.CurrentEpisode--;
                    message = "Went back an EPISODE";
                }

                context.SaveChanges();
            }
            TempData["message"] = message;
            return RedirectToAction("ByShow", new { showID = show.ShowID });
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

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using we_watch.Models;

namespace we_watch.Controllers
{
    public class ShowController : Controller
    {
        public IActionResult Index()
        {
            if (!isLoggedIn()) { return RedirectToAction("Login", "User"); }


            List<string> allShowTitles = new List<string>();
            using (WeWatchContext context = new WeWatchContext())
            {

                allShowTitles = context.Show.Select(x=> x.Title).ToList();

            }
            ViewBag.AllShows = allShowTitles;
            return View();
        }

        public IActionResult ManageShows()
        {
            if (!isLoggedIn()) { return RedirectToAction("Login", "User"); }

                List<ShowSeason> Seasons;
            using (WeWatchContext context = new WeWatchContext())
            {
                List<Show> allShows = new List<Show>();

                allShows = context.Show.OrderBy(x => x.Title).ToList();
                foreach (Show show in allShows)
                {
                    Seasons = context.ShowSeason.Where(x => x.ShowID == show.ShowID).OrderBy(x => x.IndividualSeason).ToList();
                }
                ViewBag.AllShows = allShows;

            }
            // Citation
            //https://stackoverflow.com/questions/14497711/set-viewbag-before-redirect
            // Used for transfering error/success messages between redirects.
            // Ensure that null coalesing is added to TempData before using the ToString() method to avoid null reference errors

            ViewBag.messages = TempData["message"]?.ToString();

          

            return View();
        }

        [HttpPost]
        // This action Adds a show to the database
        public IActionResult AddShow(string title, int indSeason, int episodes)
        {
            if (!isLoggedIn()) { return RedirectToAction("Login", "User"); }

            string messages;
            using (WeWatchContext context = new WeWatchContext())
            {
                if (context.Show.Where(x => x.Title.ToUpper() == title.ToUpper().Trim()).Count() > 0)
                {
                    messages = ("Program already exists");
                }
                else if (indSeason < 1)
                {
                    messages = ("Seasons can only be positive numbers");
                }
                else if (episodes < 1)
                {
                    messages = ("Episodes can only be positive numbers");
                }

                else
                {

                    Show newShow = new Show() { Title = title.Trim() };
                    context.Show.Add(newShow);
                    context.SaveChanges();
                    int showID = context.Show.Where(x => x.Title == title).Single().ShowID;
                    ShowSeason newSeason = new ShowSeason() { ShowID = showID, IndividualSeason = indSeason, SeasonEpisodes = episodes };
                    context.ShowSeason.Add(newSeason);
                    context.SaveChanges();
                    messages = ("Successfully added Program");
                }
            }

            TempData["message"] = messages;
            return Redirect("ManageShows");
        }

        [HttpPost]
        public IActionResult DeleteSeason(int deleteSeason)
        {
            if (!isLoggedIn()) { return RedirectToAction("Login", "User"); }

            string messages;
            using (WeWatchContext context = new WeWatchContext())
            {
                string tempTitle;
                if (deleteSeason == 0)
                { messages = "Cannot delete unknown show. Please refresh and try again"; }
                else
                {
                    ShowSeason season = context.ShowSeason.Where(x => x.ShowSeasonID == deleteSeason).SingleOrDefault();
                    if (season == null)
                    { messages = "Cannot find show. Please refresh and try again"; }
                    else
                    {
                        tempTitle = context.Show.Where(x => x.ShowID == season.ShowID).Select(y => y.Title).SingleOrDefault().ToString();
                        context.ShowSeason.Remove(season);
                        context.SaveChanges();
                        messages = $"Successfully deleted Season number {season.IndividualSeason} from {tempTitle}";
                    }
                }
            }
            TempData["message"] = messages;
            return Redirect("ManageShows");

        }
        [HttpPost]
        public IActionResult DeleteProgram(int showID)
        {
            if (!isLoggedIn()) { return RedirectToAction("Login", "User"); }

            string message;
            using (WeWatchContext context = new WeWatchContext())
            {
                string tempTitle = context.Show.Where(x => x.ShowID == showID).Select(y => y.Title).Single().ToString();
                if (showID == 0)
                { message = "No show was provided."; }
                else
                {
                    Show show = context.Show.Where(x => x.ShowID == showID).SingleOrDefault();
                    if (show == null)
                    { message = "Cannot delete show. Please try again later"; }
                    else
                    {
                        context.Show.Remove(show);
                        context.SaveChanges();
                        message = $"Successfully deleted {tempTitle}";
                    }
                }
            }
            TempData["message"] = message;
            return Redirect("ManageShows");

        }

        public IActionResult EditTitle(string title, int showID)
        {
            if (!isLoggedIn()) { return RedirectToAction("Login", "User"); }

            string message;
            using (WeWatchContext context = new WeWatchContext())
            {
                string tempTitle;
                if (showID == 0)
                { message = "No show was provided."; }
                else
                {
                    Show show = context.Show.Where(x => x.ShowID == showID).SingleOrDefault();
                    if (show == null)
                    { message = "Cannot delete show. Please refresh and try again"; }
                    else
                    {
                        tempTitle = show.Title;
                        show.Title = title;
                        context.SaveChanges();
                        message = $"Successfully updated '{tempTitle}' to '{title}'";
                    }
                }
            }
            TempData["message"] = message;
            return Redirect("ManageShows");

        }
        public IActionResult AddSeason(int showID, string newSeason, string newEpisodes)
        {
            if (!isLoggedIn()) { return RedirectToAction("Login", "User"); }

            string message = null;

            using (WeWatchContext context = new WeWatchContext())
            {
                Show show = context.Show.Where(x => x.ShowID == showID).SingleOrDefault();

                if (!int.TryParse(newSeason, out int parsedNewSeason))
                {
                    //ViewBag.Message = "No";
                    message = "Seasons must be positive valid numbers";
                }
                else if (parsedNewSeason < 1 || parsedNewSeason > 50)
                {
                    message = "Season must be between 1 and 50";
                }
                else if (!int.TryParse(newEpisodes, out int parsedNewEpisodes))
                {
                    message = "Episodes must be positive valid numbers";
                }
                else if (parsedNewEpisodes < 1 || parsedNewEpisodes > 50)
                {
                    message = "Episodes must be between 1 and 50";
                }

                else if (context.ShowSeason.Where(x => x.ShowID == showID).Where(x => x.IndividualSeason == parsedNewSeason).SingleOrDefault() != null)
                {
                    message = $"Season already exists in {show.Title}";
                }
                else
                {
                    ShowSeason showSeason = new ShowSeason() { ShowID = showID, IndividualSeason = parsedNewSeason, SeasonEpisodes = parsedNewEpisodes };
                    context.ShowSeason.Add(showSeason);
                    context.SaveChanges();
                    message = $"Successfully added season { parsedNewSeason } to {show.Title}";
                }
            }
            TempData["message"] = message;
            return Redirect("ManageShows");
        }
        [HttpPost]
        public IActionResult EditSeason(int editSeason, string season, string episodes)
        {

            if (!isLoggedIn()) { return RedirectToAction("Login", "User"); }

            using (WeWatchContext context = new WeWatchContext())
            {
             

                string message;
                int tempSeason;
                int tempEpisodes;
                if (editSeason == 0)
                { 
                    message = "Cannot edit unknown season. Please refresh and try again"; 
                }
                else
                {
                    ShowSeason targetSeason = context.ShowSeason.Where(x => x.ShowSeasonID == editSeason).SingleOrDefault();
                    if (season == null)
                    {
                        message = "Cannot find that season. Please refresh and try again";
                    }

                    else
                    {
                        Show show = context.Show.Where(x => x.ShowID == targetSeason.ShowID).SingleOrDefault();

                        if (show == null)
                        {
                            message = "Cannot find that show. Please refresh and try again.";
                        }
                        else
                        {
                            if (!int.TryParse(season, out int parsedNewSeason))
                            {
                                message = "Seasons must be positive valid numbers";
                            }
                            else if (parsedNewSeason < 1 || parsedNewSeason > 50)
                            {
                                message = "Season must be between 1 and 50";
                            }
                            else if (!int.TryParse(episodes, out int parsedNewEpisodes))
                            {
                                message = "Episodes must be positive valid numbers";
                            }
                            else if (parsedNewEpisodes < 1 || parsedNewEpisodes > 50)
                            {
                                message = "Episodes must be between 1 and 50";
                            }

                            else if (parsedNewSeason != targetSeason.IndividualSeason && context.ShowSeason.Where(x => x.ShowID == targetSeason.ShowID).Where(x => x.IndividualSeason == parsedNewSeason).SingleOrDefault() != null)
                            {
                                message = $"Season already exists in {show.Title}";
                            }
                            else
                            {
                                tempSeason = targetSeason.IndividualSeason;
                                tempEpisodes = targetSeason.SeasonEpisodes;
                                targetSeason.IndividualSeason = parsedNewSeason;
                                targetSeason.SeasonEpisodes = parsedNewEpisodes;
                                context.SaveChanges();
                                message = $"Successfully changed {show.Title} from season {tempSeason} having {tempEpisodes} episodes to season {parsedNewSeason} having {parsedNewEpisodes} ";
                            }
                        }
                    }
                }
                
                TempData["message"] = message;
                return Redirect("ManageShows");

            }
        }

        public bool isLoggedIn()
        {
            return (HttpContext.Session.GetString("isLoggedIn") == "true" );
        }
    }
}

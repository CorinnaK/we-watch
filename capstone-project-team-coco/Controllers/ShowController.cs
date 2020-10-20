using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using we_watch.Models;

namespace we_watch.Controllers
{
    public class ShowController : Controller
    {
        // Index lists all the shows in the database
        public IActionResult Index()
        {
            return RedirectToAction("ManageShows");
        }


        // Central View for CRUD relating to shows or seasons
        public IActionResult ManageShows()
        {
            // Ensure user is logged in
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
                if (title.Length > 50)
                {
                    messages = ("Program name cannot be greater than 50 characters.");
                }
                else if (context.Show.Where(x => x.Title.ToUpper() == title.ToUpper().Trim()).Count() > 0)
                {
                    messages = ($"{title} already exists.");
                }

                else if (indSeason < 1 || indSeason > 50)
                {
                    messages = ("Seasons must be between 1 and 50.");
                }
                else if (episodes < 1)
                {
                    messages = ("Episodes must be between 1 and 50.");
                }

                else
                {
                    Show newShow = new Show() { Title = title.Trim() };
                    context.Show.Add(newShow);
                    context.SaveChanges();
                    int showID = context.Show.Where(x => x.Title == title.Trim()).Single().ShowID;
                    ShowSeason newSeason = new ShowSeason() { ShowID = showID, IndividualSeason = indSeason, SeasonEpisodes = episodes };
                    context.ShowSeason.Add(newSeason);
                    context.SaveChanges();
                    messages = ($"Successfully added {newShow.Title}");
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

                // Make sure a value was passed for the seasonID
                if (deleteSeason == 0)
                { messages = "Cannot delete unknown program. Please refresh and try again."; }

                else
                {
                    // Check to see if the passed value matches a seasonID
                    ShowSeason season = context.ShowSeason.Where(x => x.ShowSeasonID == deleteSeason).SingleOrDefault();

                    if (season == null)
                    { messages = "Cannot find program. Please refresh and try again."; }

                    // Check that if there is a show card with that season referenced as watching
                    else if (context.ShowCard.Where(x => x.CurrentSeason == season.ShowSeasonID).Count() > 0)
                    {
                        messages = "Cannot delete a season with a Show Card reference.";
                    }

                    // We can delete the season
                    else
                    {
                        tempTitle = context.Show.Where(x => x.ShowID == season.ShowID).Select(y => y.Title).SingleOrDefault().ToString();
                        context.ShowSeason.Remove(season);
                        context.SaveChanges();
                        messages = $"Successfully deleted Season number {season.IndividualSeason} from {tempTitle}";
                    }
                }
            }

            // Return messages
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
                if (showID == 0)
                { message = "No program was provided."; }

                else
                {
                    Show show = context.Show.Where(x => x.ShowID == showID).SingleOrDefault();
                    if (show == null)
                    { message = "Cannot delete program. Please refresh and try again."; }

                    else
                    {
                        // Store the title in a temp variable to reference in messages to the user
                        string tempTitle = show.Title;
                        context.Show.Remove(show);
                        context.SaveChanges();
                        message = $"Successfully deleted {tempTitle}";
                    }
                }
            }
            TempData["message"] = message;
            return Redirect("ManageShows");
        }

        [HttpPost]
        public IActionResult EditTitle(string title, int showID)
        {
            if (!isLoggedIn()) { return RedirectToAction("Login", "User"); }

            string message;

            using (WeWatchContext context = new WeWatchContext())
            {
                if (showID == 0)
                { message = "No program was provided."; }

                else if (title == null)
                { message = "No title was provided."; }

                else
                {
                    title = title.Trim();

                    Show show = context.Show.Where(x => x.ShowID == showID).SingleOrDefault();

                    if (show == null)
                    { message = "Program not found. Please refresh and try again."; }

                    else if (title.Count() > 50)
                    { message = "Program name cannot be more than 50 characters."; }

                    else if (show.Title.ToUpper() == title.ToUpper())
                    { message = $"No changes were detect for program title {title}."; }

                    else if (context.Show.Where(x => x.Title.ToUpper() == title.ToUpper()).Count() > 0)
                    { message = $"{title} already exists."; }

                    else
                    {
                        string tempTitle = show.Title;
                        show.Title = title;
                        context.SaveChanges();
                        message = $"Successfully updated '{tempTitle}' to '{title}'.";
                    }
                }
            }

            TempData["message"] = message;
            return Redirect("ManageShows");

        }

        [HttpPost]
        public IActionResult AddSeason(int showID, string newSeason, string newEpisodes)
        {
            if (!isLoggedIn()) { return RedirectToAction("Login", "User"); }

            string message = null;

            using (WeWatchContext context = new WeWatchContext())
            {
                Show show = context.Show.Where(x => x.ShowID == showID).SingleOrDefault();

                if (!int.TryParse(newSeason, out int parsedNewSeason))
                {
                    message = "Seasons must be positive valid numbers.";
                }
                else if (parsedNewSeason < 1 || parsedNewSeason > 50)
                {
                    message = "Season must be between 1 and 50.";
                }
                else if (!int.TryParse(newEpisodes, out int parsedNewEpisodes))
                {
                    message = "Episodes must be positive valid numbers.";
                }
                else if (parsedNewEpisodes < 1 || parsedNewEpisodes > 50)
                {
                    message = "Episodes must be between 1 and 50.";
                }

                else if (context.ShowSeason.Where(x => x.ShowID == showID).Where(x => x.IndividualSeason == parsedNewSeason).SingleOrDefault() != null)
                {
                    message = $"Season already exists in {show.Title}";
                }
                else
                {
                    ShowSeason showSeason = new ShowSeason()
                    {
                        ShowID = showID,
                        IndividualSeason =
                      parsedNewSeason,
                        SeasonEpisodes = parsedNewEpisodes
                    };

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
                            message = "Cannot find that program. Please refresh and try again.";
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

                            // To avoid giving a false error when applying the no season duplicate constraint if the user is only editting the episodes, we must first check that the new season and the season that they are attempting to submit are not the same. If they are not we can continue to check if there is a duplicate.

                            else if (parsedNewSeason != targetSeason.IndividualSeason && context.ShowSeason.Where(x => x.ShowID == targetSeason.ShowID).Where(x => x.IndividualSeason == parsedNewSeason).SingleOrDefault() != null)
                            {
                                message = $"Season already exists in {show.Title}";
                            }

                            else if (parsedNewSeason == targetSeason.IndividualSeason && parsedNewEpisodes == targetSeason.SeasonEpisodes)
                            {
                                message = $"No Changes were detected for {show.Title} - Season {targetSeason.IndividualSeason}.";
                            }

                            else if (context.ShowCard.Where(x => x.CurrentSeason == targetSeason.ShowSeasonID).Count() > 0)
                            {
                                message = "Cannot edit a season with a Show Card reference.";
                            }

                            else
                            {
                                tempSeason = targetSeason.IndividualSeason;
                                tempEpisodes = targetSeason.SeasonEpisodes;
                                targetSeason.IndividualSeason = parsedNewSeason;
                                targetSeason.SeasonEpisodes = parsedNewEpisodes;
                                context.SaveChanges();
                                message = $"Successfully changed {show.Title} from season {tempSeason} having {tempEpisodes} episodes to season {parsedNewSeason} having {parsedNewEpisodes} episodes.";
                            }
                        }
                    }
                }

                TempData["message"] = message;
                return Redirect("ManageShows");

            }
        }

        [HttpPost]
        public bool isLoggedIn()
        {
            return (HttpContext.Session.GetString("isLoggedIn") == "true");
        }
    }
}

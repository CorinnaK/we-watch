﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using we_watch.Models;

namespace we_watch.Controllers
{
    public class ShowController : Controller
    {
        public IActionResult ManageShows()
        { 
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

        public IActionResult AddShow(string title, int indSeason, int episodes)
        {

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

            return RedirectToAction("ManageShows", new {  messages });
        }

        [HttpPost]

        // Capture the data in the fields for the Manages Shows form. Title, Season Number, Episodes can be editted. Input values for the buttons are deleteProgram, deleteSeason, saveChanges. ShowID and SeasonID are constants to the form and are hidden input fields used for identifying the values in the database. 
        public IActionResult Edit(string title, int showID, string saveChanges, string[] seasonID, string[] episodes, string[] season)
        {
            string message = $"This did not Work";

            // The Save button was selected. Error checking to ensure no values are null.
            if (saveChanges == "Save")
            {
                message = $"Your Show ID is {showID}, The seasonID is {seasonID}, the season number is {season} and the episodes are {episodes[2]}";
                if (title == null)
                    message = "Title cannot be blank";
            }

            return RedirectToAction("ManageShows", new { messages = message });
        }

        public IActionResult DeleteSeason(int deleteSeason)
        {
            string message;
            using (WeWatchContext context = new WeWatchContext())
            {
                string tempTitle;
                if (deleteSeason == 0)
                { message = "Cannot delete unknown show. Please refresh and try again"; }
                else
                {
                    ShowSeason season = context.ShowSeason.Where(x => x.ShowSeasonID == deleteSeason).SingleOrDefault();
                    if (season == null)
                    { message = "Cannot find show. Please refresh and try again"; }
                    else
                    {
                        tempTitle = context.Show.Where(x => x.ShowID == season.ShowID).Select(y => y.Title).SingleOrDefault().ToString();
                        context.ShowSeason.Remove(season);
                        context.SaveChanges();
                        message = $"Successfully deleted Season number {season.IndividualSeason} from {tempTitle}";
                    }
                }
            }
            TempData["message"] = message;
            return Redirect("ManageShows");

        }

        public IActionResult DeleteProgram(int showID)
        {
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

        static string EditProgram(string title,  int seasonID, int seasonNum, int episodes)
        {
            string message;
            using (WeWatchContext context = new WeWatchContext())
            {
                
                if (seasonID == 0)
                { message = "No show was provided."; }
                else
                {
                    ShowSeason season = context.ShowSeason.Where(x => x.ShowSeasonID == seasonID).SingleOrDefault();
                    if (season == null)
                    { message = "Cannot find season in the database. Please refresh and try again."; }
                    else
                    {
                        Show show = context.Show.Where(x => x.ShowID == season.ShowID).SingleOrDefault();

                        show.Title = title;
                        season.IndividualSeason = seasonNum;
                        season.SeasonEpisodes = episodes;
                        context.SaveChanges();
                        message = "Success";
                    }
                }
            }

            return message;
        }
        public IActionResult AddSeason(int showID, string newSeason, string newEpisodes)
        {
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

    }
}

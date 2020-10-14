﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using we_watch.Models;

namespace we_watch.Controllers
{
    public class ShowController : Controller
    {
        public IActionResult ManageShows(string messages)
        {
            List<ShowSeason> Seasons;
            using (WeWatchContext context = new WeWatchContext())
            {
                List<Show> allShows = new List<Show>();
             
                allShows = context.Show.OrderBy(x => x.Title).ToList();
                foreach (Show show in allShows)
                {
                    Seasons = context.ShowSeason.Where(x => x.ShowID == show.ShowID).ToList();
                }
                ViewBag.AllShows = allShows;

            }
            ViewBag.messages = messages;
                return View();
        }

    [HttpPost]
    public IActionResult AddShow(string title, string platform, int indSeason, int episodes)
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

                Show newShow = new Show() { Title = title.Trim(), TotalSeasons = 1 };
                context.Show.Add(newShow);
                context.SaveChanges();
                int showID = context.Show.Where(x => x.Title == title).Single().ShowID;
                ShowSeason newSeason = new ShowSeason() { ShowID = showID, IndividualSeason = indSeason, SeasonEpisodes = episodes };
                context.ShowSeason.Add(newSeason);
                context.SaveChanges();
               messages = ("Successfully added Program");
            }
        }

        return RedirectToAction("ManageShows", new { messages = messages});
    }

        [HttpPost]

        // Capture the data in the fields for the Manages Shows form. Title, Season Number, Episodes can be editted. Input values for the buttons are deleteProgram, deleteSeason, saveChanges. ShowID and SeasonID are constants to the form and are hidden input fields used for identifying the values in the database. 
        public IActionResult Edit(string title, int showID, string deleteProgram, string[] deleteSeason, string saveChanges, string[] seasonID, string[] episodes, string[] season)
        {
            string message = $"This did not Work";

            // The delete Program button was selected.
            if (deleteProgram == "DELETE")
                message = DeleteProgram(showID);

            // The X was selected to delete a Season.
            else if (deleteSeason.Count() != 0 )
            {
                int parsedSeasonID = int.Parse(seasonID[deleteSeason.Count()]);
                message = DeleteSeason(parsedSeasonID);
                }

            // The Save button was selected. Error checking to ensure no values are null.
            else if (saveChanges == "Save")
            { message = $"Your Show ID is {showID}, The seasonID is {seasonID}, the season number is {season} and the episodes are {episodes[2]}";
                if (title == null)
                    message = "Title cannot be blank";
              //  if (seasonID < 1)
                //    message = "Season must be a positive number between 1-50"; 
               // if (episodes < 1)
                 //   message = "Episodes must be a positive number between 1-50";
              //  message = EditProgram(title, showID, seasonID, season, episodes);          
            }
                
                return RedirectToAction("ManageShows", new { messages = message});
        }

        static string DeleteProgram(int showID)
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
            return message;
            
        }

        static string DeleteSeason(int seasonID)
        {
            string message;
            using (WeWatchContext context = new WeWatchContext())
            {
                string tempTitle;
                if (seasonID == 0)
                { message = "Cannot delete unknown show."; }
                else
                {
                    ShowSeason season = context.ShowSeason.Where(x => x.ShowSeasonID == seasonID).SingleOrDefault();
                    if (season == null)
                    { message = "Cannot delete show. Please try again later"; }
                    else
                    {
                        tempTitle = context.Show.Where(x => x.ShowID == season.ShowID).Select(y => y.Title).SingleOrDefault().ToString();
                        context.ShowSeason.Remove(season);
                        context.SaveChanges();
                        message = $"Successfully deleted Season number {season.IndividualSeason} from {tempTitle}";
                    }
                }
            }
            return message;

        }

        static string EditProgram(string title, int showID, int seasonID, int seasonNum, int episodes)
        {
            string message;
            using (WeWatchContext context = new WeWatchContext())
            {
                string tempTitle;
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
        static bool ValidatePositiveInRange(string stringNum)
        {
            
            int.TryParse(stringNum, out int number);
            
        
        
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using we_watch.Models;

namespace we_watch.Controllers
{
    public class ShowController : Controller
    {
        public IActionResult AddShow()
        {
            return View();
        }
           
        [HttpPost]
            public IActionResult AddShow(string title, string platform, int indSeason, int episodes)
        {

            using (WeWatchContext context = new WeWatchContext())
            {
                if (context.Show.Where(x => x.Title == title).Count() > 0)
                {
                    ViewBag.titleError = "Program already exists";
                }
                else if (indSeason < 1)
                {
                    ViewBag.indSeasonError = "Seasons can only be positive numbers";
                }
                else if (episodes < 1)
                {
                    ViewBag.episodeError = "Episodes can only be positive numbers";
                }

                else
                {

                    Show newShow = new Show() { Title = title, TotalSeasons = 1 };
                    context.Show.Add(newShow);
                    context.SaveChanges();
                   int showID = context.Show.Where(x => x.Title == title).Single().ShowID;
                    ShowSeason newSeason = new ShowSeason() { ShowID = showID, IndividualSeason = indSeason, SeasonEpisodes = episodes};
                    context.ShowSeason.Add(newSeason);
                    context.SaveChanges();
                    ViewBag.Success = "Successfully added Program";
                }
            }

            return View();
        }
    }
}

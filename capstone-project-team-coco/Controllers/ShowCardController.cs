using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using we_watch.Models;
namespace we_watch.Controllers
{
    public class ShowCardController : Controller
    {
        public IActionResult Index()
        {

            List<Show> allShows = new List<Show>();
            using (WeWatchContext context = new WeWatchContext())
            {
                allShows = context.Show.OrderBy(x => x.Title).ToList();

            }
            ViewBag.AllShows = allShows;
            return View();
        }


        public IActionResult ManageCards(int showID, int watcherID, int seasonID, int episode)
        {
            using (WeWatchContext context = new WeWatchContext())
            {

                Show selectedShow = new Show();
                ShowSeason selectedSeason = new ShowSeason();
                Watcher selectedWatcher = new Watcher();
                List<ShowSeason> seasons = new List<ShowSeason>();

                if (showID != 0)
                {
                    selectedShow = context.Show.Where(x => x.ShowID == showID).Single();
                    seasons = context.ShowSeason.Where(x => x.ShowID == showID).ToList();
                    if (seasonID != 0)
                    {
                        selectedSeason = context.ShowSeason.Where(x => x.ShowSeasonID == seasonID).Single();
                    }
                }
                if (watcherID != 0)
                {
                    selectedWatcher = context.Watcher.Where(x => x.WatcherID == watcherID).Single();
                }


                List<Show> allShows = context.Show.OrderBy(x => x.Title).ToList();
                List<Watcher> allWatchers = context.Watcher.OrderBy(x => x.Name).ToList();

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

        public IActionResult Connect(int showID, int watcherID, int showSeasonID, int episode)
        {

            return View();
        }
    }
}

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
    }
}

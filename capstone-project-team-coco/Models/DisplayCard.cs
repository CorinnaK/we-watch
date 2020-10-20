using System.Linq;

namespace we_watch.Models
{
    public class DisplayCard
    {
        public int ShowCardID { get; set; }
        private int _seasonID;
        public int ShowID { get; set; }
        public string ShowTitle { get; set; }
        public int WatcherID;
        public string WatcherName { get; set; }
        public int SeasonID { get => _seasonID;
            set
            {
                using WeWatchContext context = new WeWatchContext();
                _seasonID = value;
                CurrentSeason = context.ShowSeason.Where(x => x.ShowSeasonID == value).Select(x => x.IndividualSeason).Single();
                Episodes = context.ShowSeason.Where(x => x.ShowSeasonID == value).Select(x => x.SeasonEpisodes).Single();
            }

        }
        public int CurrentSeason { get; private set; }
        public int Episodes { get; private set; }
        public int CurrentEpisode { get; set; }

        public DisplayCard() { }

        public DisplayCard(int showID, int watcherID)
        {
            using WeWatchContext context = new WeWatchContext();
            ShowID = showID;
            WatcherID = watcherID;
            ShowTitle = context.Show.Where(x => x.ShowID == showID).Select(x => x.Title).Single().ToString();
            WatcherName = context.Watcher.Where(x => x.WatcherID == watcherID).Select(x => x.Name).Single().ToString();
        }

    }
}

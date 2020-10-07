using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace we_watch.Models
{
    [Table("watch_history")]
    public partial class WatchHistory
    {

        [Key]
        [Column("WatchHistoryID", TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int WatchHistoryID { get; set; }

  
        [Column("SeasonNum", TypeName = "smallint(2)")]
        [Required]
        public int SeasonNum { get; set; }


        [Column("EpisodeNum", TypeName = "smallint(2)")]
        [Required]
        public int Episode_Num { get; set; }

        public string Platform { get; set; }

       
        [InverseProperty(nameof(Models.ShowCard.WatchHistories))]
        public virtual ShowCard ShowCard { get; set; } // Watcher_History has one show card)

    }
}

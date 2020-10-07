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

        [Column("ShowCardID", TypeName = "int(10)")]
        [Required]
        public int ShowCardID { get; set; }

        [Column("SeasonNum", TypeName = "tinyint(2)")]
        [Required]
        public int SeasonNum { get; set; }

        [Column("EpisodeNum", TypeName = "tinyint(2)")]
        [Required]
        public int Episode_Num { get; set; }

        [Column("Platform", TypeName = "varchar(20)")]
        public string Platform { get; set; }

        [ForeignKey(nameof(ShowCardID))]
        [InverseProperty(nameof(Models.ShowCard.WatchHistories))]
        public virtual ShowCard ShowCard { get; set; }


    }
}

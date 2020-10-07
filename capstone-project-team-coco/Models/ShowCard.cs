
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace we_watch.Models
{
    [Table("ShowCard")]
    public partial class ShowCard
    {
        public ShowCard()
        {
            WatchHistories = new HashSet<WatchHistory>();
        }


        [Key]
        [Column("ShowCardID", TypeName = "int(10)")] // Composite ID
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ShowCardID { get; set; }

        [Column("ShowID", TypeName = "int(10)")]
        [Required]
        public int ShowID { get; set; }

        [Column("WatcherID", TypeName = "int(10)")]
        [Required]
        public int WatcherID { get; set; }        

        [Column("Platform", TypeName = "varchar(20)")]        
        public string Platform { get; set; }

        [Column("CurrentSeason", TypeName = "smallint(2)")]
        [Required]
        public int CurrentSeason { get; set; }
        
        [Column("CurrentEpisode", TypeName = "smallint(2)")]
        [Required]
        public int CurrentEpisode { get; set; }

        [Column("Status", TypeName = "varchar(20)")]
        [Required]
        [DefaultValue("Current")] // To set a default value of "current" for Status column
        public string Status { get; set; }

        [ForeignKey(nameof(ShowID))]
        // InverseProperty links the two virtual properties together.
        [InverseProperty(nameof(Models.Show.ShowCards))]  // [] are attributes that will be connected to any variable directly following it
        public virtual Show Show { get; set; } // sets a property that links back to the specified foreign key; data type is show, and property is also show
        

        [ForeignKey(nameof(WatcherID))]
        [InverseProperty(nameof(Models.Watcher.ShowCards))]
        public virtual Watcher Watcher { get; set; }


        [InverseProperty(nameof(Models.WatchHistory.ShowCard))]
        public virtual ICollection<WatchHistory> WatchHistories { get; set; }

    }
}

﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
        [Column("ShowCardID", TypeName = "int(10)")] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ShowCardID { get; set; }


        [Column("UserID", TypeName = "int(10)")]
        [Required]
        public int UserID { get; set; }


        [Column("ShowID", TypeName = "int(10)")]
        [Required]
        public int ShowID { get; set; }


        [Column("WatcherID", TypeName = "int(10)")]
        [Required]
        public int WatcherID { get; set; }        


        [Column("CurrentSeason", TypeName = "smallint(2)")]
        [Required]
        public int CurrentSeason { get; set; }
        

        [Column("CurrentEpisode", TypeName = "smallint(2)")]
        [Required]
        public int CurrentEpisode { get; set; }


        // [] are attributes that will be connected to any variable directly following it
        [ForeignKey(nameof(ShowID))] 
       
        // InverseProperty links the two virtual properties together.
        [InverseProperty(nameof(Models.Show.ShowCards))] 
       
        // sets a property that links back to the specified foreign key; data type is show, and property is also show
        public virtual Show Show { get; set; } 
        

        [ForeignKey(nameof(WatcherID))]
        [InverseProperty(nameof(Models.Watcher.ShowCards))]
        public virtual Watcher Watcher { get; set; }


        [ForeignKey(nameof(UserID))]
        [InverseProperty(nameof(Models.User.ShowCards))]
        public virtual User User { get; set; }


        [InverseProperty(nameof(Models.WatchHistory.ShowCard))]
        public virtual ICollection<WatchHistory> WatchHistories { get; set; }

    }
}

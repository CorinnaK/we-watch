﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace we_watch.Models
{
    // Citation:
    //https://github.com/TECHCareers-by-Manpower/4.1-MVC/tree/Sep22Practice/MVC_4Point1/Models
    // Used in class practice coded by James as reference for Model Creation

    [Table("Watcher")]
    public partial class Watcher
    {

        [Key]
        [Column("WatcherID", TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int WatcherID { get; set; }

        [Column("Name", TypeName = "varchar(30)")]
        [Required]
        public string Name { get; set; }

        // Watcher can have a collection of multiple show cards (one watcher to many show cards)
       [InverseProperty(nameof(Models.ShowCard.Watcher))]
        public virtual ICollection<ShowCard> ShowCards { get; set; } 

    }
}

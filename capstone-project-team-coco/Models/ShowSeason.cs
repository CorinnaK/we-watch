﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace we_watch.Models
{
    // Citation:
    //https://github.com/TECHCareers-by-Manpower/4.1-MVC/tree/Sep22Practice/MVC_4Point1/Models
    // Used in class practice coded by James as reference for Model Creation

    [Table("ShowSeason")]
    public partial class ShowSeason
    {

        [Key]
        [Column("ShowSeasonID", TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ShowSeasonID { get; set; }
       
        [Column("ShowID", TypeName = "int(10)")]
        [Required]
        public int ShowID { get; set; }

        [Column("IndividualSeason", TypeName = "smallint(2)")]
        [Required]
        public int IndividualSeason { get; set; }
        
        [Column("SeasonEpisodes", TypeName = "smallint(2)")]
        [Required]
        public int SeasonEpisodes { get; set; }

        [ForeignKey(nameof(ShowID))]

        // InverseProperty links the two virtual properties together.
        [InverseProperty(nameof(Models.Show.ShowSeasons))]  
        
        // [] are attributes that will be connected to any variable directly following it; 
        public virtual Show Show { get; set; } 
        
       

       


    }
}

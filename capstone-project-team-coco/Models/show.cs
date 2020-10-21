using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace we_watch.Models
{
    // Citation:
    //https://github.com/TECHCareers-by-Manpower/4.1-MVC/tree/Sep22Practice/MVC_4Point1/Models
    // Used in class practice coded by James as reference for Model Creation

    [Table("show")]
    public partial class Show
    {
        // This initializes an empty list so we don't get null reference exceptions for our list when program checkes for ICollection later down.
        public Show()
        {
            ShowSeasons = new HashSet<ShowSeason>();
            ShowCards = new HashSet<ShowCard>();
        }

        [Key]
        [Column("ShowID", TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ShowID { get; set; }


        [Column("Title", TypeName = "varchar(50)")]
        [Required]
        public string Title { get; set; }


        [InverseProperty(nameof(Models.ShowSeason.Show))]
        
        // ICollection is an object with lists within it
        public virtual ICollection<ShowSeason> ShowSeasons { get; set; } 

        [InverseProperty(nameof(Models.ShowCard.Show))]
        public virtual ICollection<ShowCard> ShowCards { get; set; }
    }
}

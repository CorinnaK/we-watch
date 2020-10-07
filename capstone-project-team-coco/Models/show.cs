using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace we_watch.Models
{
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

        [Column("UserID", TypeName = "int(10)")]
        [Required]
        public int UserID { get; set; }
    
        [Column("Title", TypeName = "varchar(50)")]
        [Required]
        public string Title { get; set; }


        [Column("TotalSeasons", TypeName = "smallint(2)")]
        [Required]
        public int TotalSeasons { get; set; }


        [ForeignKey(nameof(UserID))]
        [InverseProperty(nameof(Models.TheUser.Shows))]
        public virtual TheUser TheUser { get; set; }


        [InverseProperty(nameof(Models.ShowSeason.Show))]
        public virtual ICollection<ShowSeason> ShowSeasons { get; set; } // ICollection is an object with lists within it

        [InverseProperty(nameof(Models.ShowCard.Show))]
        public virtual ICollection<ShowCard> ShowCards { get; set; }
    }
}

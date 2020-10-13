

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace we_watch.Models
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            ShowCards = new HashSet<ShowCard>();
        }

        [Key]
        [Column("UserID", TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int UserID { get; set; }

        [Column("Email", TypeName = "varchar(30)")]
        [Required]
        public string Email { get; set; }

        [Column("Salt", TypeName = "varchar(32)")]
        [Required]
        public string Salt { get; set; }


        [Column("HashPassword", TypeName = "char(64)")]
        [Required]
        public string HashPassword { get; set; }

        [InverseProperty(nameof(Models.ShowCard.User))]
        public virtual ICollection<ShowCard> ShowCards { get; set; }


    }
}

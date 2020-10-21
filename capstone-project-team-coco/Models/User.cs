using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace we_watch.Models
{
    // Citation:
    //https://github.com/TECHCareers-by-Manpower/4.1-MVC/tree/Sep22Practice/MVC_4Point1/Models
    // Used in class practice coded by James as reference for Model Creation
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
        [Required(AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage ="Please enter an email")]
        public string Email { get; set; }

        [Column("Salt", TypeName = "varchar(10)")]
        [Required(AllowEmptyStrings = false)]
        public string Salt { get; set; }

        [Column("HashPassword", TypeName = "char(64)")]
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]        
        public string HashPassword { get; set; }

        [InverseProperty(nameof(Models.ShowCard.User))]
        public virtual ICollection<ShowCard> ShowCards { get; set; }


    }
}

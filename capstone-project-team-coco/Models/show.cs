
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace we_watch.Models
{
    [Table("show")]
    public partial class Show
    {
        // This initializes an empty list so we don't get null reference exceptions for our list when program checkes for ICollection later down.
        public Show()
        {
            Show_Seasons = new HashSet<Show_Season>();
        }

        [Key]
        [Column("ID", TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Show_ID { get; set; }

    
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Title { get; set; }


        [Column(TypeName = "smallint(2)")]
        [Required]
        public string Total_Seasons { get; set; }

        [InverseProperty(nameof(Models.Show_Season.Show))]
        public virtual ICollection<Show_Season> Show_Seasons { get; set; } // ICollection is an object with lists within it

    }
}

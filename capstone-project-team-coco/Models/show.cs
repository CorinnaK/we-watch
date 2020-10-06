
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

        [Key]
        [Column("ID", TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }

    
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Title { get; set; }


        [Column(TypeName = "smallint(2)")]
        [Required]
        public string Total_Seasons { get; set; }

    }
}

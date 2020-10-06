
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace we_watch.Models
{
    [Table("buddy")]
    public partial class Buddy
    {

        [Key]
        [Column("ID", TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }


        [Column(TypeName = "varchar(30)")]
        [Required]
        public string Name { get; set; }

    }
}

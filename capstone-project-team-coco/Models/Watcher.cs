
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace we_watch.Models
{
    [Table("watcher")]
    public partial class Watcher
    {

        [Key]
        [Column("ID", TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }


        [Column(TypeName = "varchar(30)")]
        [Required]
        public string Name { get; set; }


        [InverseProperty(nameof(Models.Show_Card.Watcher))] // One watcher to many show cards
        public virtual ICollection<Show_Card> Show_Cards { get; set; } // Watcher can have a collection of multiple show cards (one watcher to many show cards)

    }
}

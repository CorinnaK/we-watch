
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace we_watch.Models
{
    [Table("watch_history")]
    public partial class Watch_History
    {

        [Key]
        [Column("Date", TypeName = "timestamp(10)")] 
        [Timestamp]
        [Required]
        public DateTime ID { get; set; }

        [ForeignKey(nameof(Season_Num))]
        [Column("Season_Num", TypeName = "smallint(2)")]
        [Required]
        public int Season_Num { get; set; }

        [ForeignKey(nameof(Episode_Num))]
        [Column("Episode_Num", TypeName = "smallint(2)")]
        [Required]
        public int Episode_Num { get; set; }

        [ForeignKey(nameof(Show_ID))]
        [Column("Show_ID", TypeName = "varchar(50)")]
        [Required]
        public int Show_ID { get; set; }

        [ForeignKey(nameof(Watcher_ID))]
        [Column("Watcher_Id", TypeName = "int(10)")]
        [Required]
        public int Watcher_ID { get; set; }

        [ForeignKey(nameof(Platform))]
        [Column("Platform", TypeName = "varchar(20)")]       
        public string Platform { get; set; }

       
        [InverseProperty(nameof(Models.Show_Card.Watch_History))]
                
        
        public virtual ICollection<Show_Card> Show_Cards { get; set; } // Watcher_History can have a collection of multiple show cards (one watcher history to many show cards)


    }
}

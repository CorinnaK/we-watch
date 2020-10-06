
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
    [Table("show_card")]
    public partial class Show_Card
    {

        [Key]
        [Column("Show_Card_ID", TypeName = "int(10)")] // Composite ID
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }

        [Column("Show_ID", TypeName = "int(10)")]
        [Required]
        public string Show_ID { get; set; }

        [Column("Watcher_ID", TypeName = "int(10)")]
        [Required]
        public string Watcher_ID { get; set; }        

        [Column("Platform", TypeName = "varchar(20)")]        
        public string Platform { get; set; }

        [Column("Current_Season", TypeName = "smallint(2)")]
        [Required]
        public string Current_Season { get; set; }
        
        [Column("Current_Episode", TypeName = "smallint(2)")]
        [Required]
        public string Current_Episode { get; set; }

        [Column("Status", TypeName = "varchar(20)")]
        [Required]
        [DefaultValue("Current")] // To set a default value of "current" for Status column
        public string Status { get; set; }

        [ForeignKey(nameof(Show_ID))]

        // InverseProperty links the two virtual properties together.
        [InverseProperty(nameof(Models.Show.Show_Cards))]  // [] are attributes that will be connected to any variable directly following it; 
        public virtual Show Show { get; set; } // sets a property that links back to the specified foreign key; data type is show, and property is also show
        

        [ForeignKey(nameof(Watcher_ID))]

        [InverseProperty(nameof(Models.Watcher.Show_Cards))]
        public virtual Watcher Watcher { get; set; }






    }
}

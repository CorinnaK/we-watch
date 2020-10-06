
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace we_watch.Models
{
    [Table("show_season")]
    public partial class Show_Season
    {

        [Key]
        [Column("Show_Season_ID", TypeName = "int(10)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }
       
        [Column("Show_ID", TypeName = "int(10)")]
        [Required]
        public int Show_ID { get; set; }

        [Column("Ind_Season", TypeName = "smallint(2)")]
        [Required]
        public int Ind_Season { get; set; }
        
        [Column("Total_Episodes", TypeName = "smallint(2)")]
        [Required]
        public int Total_Episodes { get; set; }

        [ForeignKey(nameof(Show_ID))]

        // InverseProperty links the two virtual properties together.
        [InverseProperty(nameof(Models.Show.Show_Seasons))]  // [] are attributes that will be connected to any variable directly following it; 
        public virtual Show Show { get; set; } 
        
       

       


    }
}

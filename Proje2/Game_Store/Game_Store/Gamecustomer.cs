using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Store
{
    [Table("Gamecustomer")]
    public class Gamecustomer
    {
        [Key]
        public int GameCus_id { get; set; }

        [ForeignKey("Customer")]
        public int Customer_id { get; set;}

        [ForeignKey("Game")]
        public int Game_id { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Game Game { get; set; }

    }
}

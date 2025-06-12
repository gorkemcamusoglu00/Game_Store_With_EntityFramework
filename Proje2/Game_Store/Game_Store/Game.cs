using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Store
{
    [Table("Games")]
    public class Game
    {
        [Key]
        public int Game_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Game_Type { get; set; }

        [Required]
        [MaxLength(50)]
        public string Game_Name { get; set; }

        [Required]
        public decimal Game_Price { get; set; }

        public virtual ICollection<Gamecustomer> Gamecustomers { get; set; }

    }
}

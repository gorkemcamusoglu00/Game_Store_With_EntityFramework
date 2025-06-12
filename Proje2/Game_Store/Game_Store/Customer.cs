using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game_Store
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int Customer_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Customer_name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Customer_surname { get; set; }

        [Required]
        [MaxLength(15)]
        public string Customer_tel { get; set; }

        [Required]
        [MaxLength(50)]
        public string Customer_mail { get; set; }

        public virtual ICollection<Gamecustomer> Gamecustomers { get; set; }
    }
}

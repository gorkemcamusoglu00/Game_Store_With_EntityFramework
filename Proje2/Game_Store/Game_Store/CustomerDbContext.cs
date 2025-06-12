using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Game_Store
{
    public class CustomerDbContext:DbContext
    {
        public CustomerDbContext():base("name=CustomerDbContext")
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Gamecustomer> Gamecustomers { get; set; }
    }
}

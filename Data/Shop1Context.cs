using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop1.Data
{
    public class Shop1Context : DbContext
    {
        public Shop1Context (DbContextOptions<Shop1Context> options)
            : base(options)
        {
        }

        public DbSet<Shop.Models.Customer> Customer { get; set; }

        public DbSet<Shop.Models.Orders> Orders { get; set; }

        public DbSet<Shop.Models.Product> Product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "Product name")]
        [StringLength(50)]
        public string ProductName { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Purchase price")]
        //[Column(TypeName = "Purchase price")]
        public decimal PurchasePrice { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Selling price")]
        //[Column(TypeName = "Selling price")]
        public decimal SellingPrice { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}

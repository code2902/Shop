using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Orders
    {
        public int OrdersID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }

        [Display(Name = "Quantity of product")]
        public int QuantityOfProduct { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        public decimal Ukupno
        {
            get
            {
                return QuantityOfProduct * Product.SellingPrice;
            }
        }


    public Customer Customer { get; set; }
        public Product Product { get; set; }


    }
}

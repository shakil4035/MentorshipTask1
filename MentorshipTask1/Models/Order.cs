using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentorshipTask1.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set;  }
        public string  CustomerName { get; set; }
        public decimal Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
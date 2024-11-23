using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentorshipTask1.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal NumStock { get; set; }
    }
}
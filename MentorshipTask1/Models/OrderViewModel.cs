using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipTask1.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string CustomerName { get; set; }
        public decimal Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal NumStock { get; set; }

    }

    public class ViewModelApi5
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal Revenue { get; set; }

    }
    public class CustomerSummary
    {
        public string CustomerName { get; set; }
        public decimal TotalQuantity { get; set; }
    }

}

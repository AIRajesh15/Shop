using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Models
{
    public class OrderItem:BaseEntity 
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }

    }
}

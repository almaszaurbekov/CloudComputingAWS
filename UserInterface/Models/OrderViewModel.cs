using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class OrderViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public double TotalPrice { get; set;  }
    }
}
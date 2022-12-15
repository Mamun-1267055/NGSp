using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Models
{
    public class GetDataView
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime StoreDate { get; set; }
        public bool IsAvailable { get; set; }
    }
}

using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MyWebApp.Models
{
    public partial class Products
    {
        public Products()
        {
            ProductImage = new HashSet<ProductImage>();
        }

        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime StoreDate { get; set; }
        public bool? IsAvailable { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
    }
}

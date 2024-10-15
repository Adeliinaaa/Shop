using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Common.Dtos
{
    public class ManipulationProductDto
    {
        public string ProductArticleNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int QuantityInStock { get; set; }
        public string Status { get; set; } 
        public int CurrentDiscount { get; set; }
        public int MaxDiscount { get; set; }
        public string Supplier {  get; set; }
        public string Manufacturer { get; set; }
        public string ProductType { get; set; }
        public string Image { get; set; }
    }
}

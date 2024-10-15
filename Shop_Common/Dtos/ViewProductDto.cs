using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Common.Dtos
{
    public class ViewProductDto
    {
        public string ProductArticleNumber { get; set; }
        public string Name { get; set; }
        public string Measure { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string ProductType { get; set; }
        public string Supplier { get; set; }
        public string Manufacturer { get; set; }
        public int CurrentDiscount { get; set; }
        public string Status { get; set; }
        public int QuantityInStock { get; set; }
    }
}
using Shop_Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Common.Mappers
{
    internal class ProductMapper
    {
        public static ManipulationProductDto ConvertToManipulationProductDto(Product product)
        {
            return new ManipulationProductDto
            {
                ProductArticleNumber = product.ProductArticleNumber,
                Name = product.Name,
                Description = product.Description,
                Cost = product.Cost,
                QuantityInStock = product.QuantityInStock,
                Status = product.Status,
                CurrentDiscount = product.CurrentDiscount,
                MaxDiscount = product.MaxDiscount,
                Supplier = product.Supplier,
                Manufacturer = product.Manufacturer,
                ProductType = product.ProductType,
                Image = product.Image
            };
        }
    }
}

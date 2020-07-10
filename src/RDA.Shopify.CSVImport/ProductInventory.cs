using System;
using System.Collections.Generic;
using System.Text;

namespace RDA.Shopify.CSVImport
{
    public class ProductInventory
    {
        public List<Product> Products { get; internal set; } = new List<Product>();

        public Product AddProduct(Product product)
        {
            Products.Add(product);

            return product;
        }
    }
}

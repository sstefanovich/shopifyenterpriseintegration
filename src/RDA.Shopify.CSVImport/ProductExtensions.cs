using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RDA.Shopify.CSVImport
{
    public static class ProductExtensions
    {
        public static Product AddVariant(this Product product, Variant variant)
        {
            product.Variants.Add(variant);

            return product;
        }

        public static Product AddImage(this Product product, Image image)
        {
            product.Images.Add(image);

            return product;
        }

        public static Product AddTag(this Product product, string tag)
        {
            if (! product.Tags.Contains(tag))
                product.Tags.Add(tag);

            return product;
        }
    }
}

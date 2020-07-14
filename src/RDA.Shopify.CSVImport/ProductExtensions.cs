using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace RDA.Shopify.CSVImport
{
    /// <summary>
    /// Convenience extensions to the Product
    /// </summary>
    public static class ProductExtensions
    {
        /// <summary>
        /// Add a variant to a product
        /// </summary>
        /// <param name="product">The product the variant is to be added to</param>
        /// <param name="variant">The variant</param>
        /// <param name="copyDataFromProduct">Indicates if Product data should be used to fill in any missing variant data (e.g. Price)</param>
        /// <returns>Product</returns>
        public static Product AddVariant(this Product product, Variant variant, bool copyDataFromProduct)
        {
            if (copyDataFromProduct)
            {
                //Generate a SKU if not set
                if (variant.SKU is null)
                    variant.SKU = $"{product.SKU}-{product.Variants.Count() + 1}";

                if (!variant.Price.HasValue)
                    variant.Price = product.Price;

                if (!variant.InventoryQuantity.HasValue)
                    variant.InventoryQuantity = product.InventoryQuantity;

                if (!variant.Grams.HasValue)
                    variant.Grams = product.Grams;

                if (!variant.CompareAtPrice.HasValue)
                    variant.CompareAtPrice = product.CompareAtPrice;

                if (!variant.CostPerItem.HasValue)
                    variant.CostPerItem = product.CostPerItem;

                if (!variant.Taxable.HasValue)
                    variant.Taxable = product.Taxable;

                if (!variant.RequiresShipping.HasValue)
                    variant.RequiresShipping = product.RequiresShipping;

                if (variant.Barcode is null)
                    variant.Barcode = product.Barcode;

                if (variant.WeightUnit is null)
                    variant.WeightUnit = product.WeightUnit;

                if (variant.InventoryTracker is null)
                    variant.InventoryTracker = product.InventoryTracker;

                if (variant.InventoryPolicy is null)
                    variant.InventoryPolicy = product.InventoryPolicy;

                if (variant.FulfillmentService is null)
                    variant.FulfillmentService = product.FulfillmentService;

                if (variant.TaxCode is null)
                    variant.TaxCode = product.TaxCode;
            }

            product.Variants.Add(variant);

            return product;
        }

        /// <summary>
        /// Adds an image to a product. Not variant specific
        /// </summary>
        /// <param name="product">The Product the image wil be added to</param>
        /// <param name="image">The Url of the image. Will be downloaded on import</param>
        /// <returns></returns>
        public static Product AddImage(this Product product, Image image)
        {
            product.Images.Add(image);

            return product;
        }

        /// <summary>
        /// Adds a tag to a product
        /// </summary>
        /// <param name="product">The product the tag will be added to</param>
        /// <param name="tag">The tag to add</param>
        /// <returns>The Product</returns>
        public static Product AddTag(this Product product, string tag)
        {
            if (! product.Tags.Contains(tag))
                product.Tags.Add(tag);

            return product;
        }
    }
}

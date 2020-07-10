using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDA.Shopify.CSVImport
{
    public static class InventoryTrackers
    {
        public static string AmazonMarketPlace = "amazon_marketplace_web";
        public static string Shipwire = "shipwire";
        public static string Shopify = "shopify";
    }

    public static class InventoryPolicies
    {
        public static string Deny = "deny";
        public static string Continue = "continue";
    }

    public static class WeigthUnits
    {
        public static string Gram = "g";
        public static string Kilogram = "kg";
        public static string Pound = "lb";
        public static string Ounce = "oz";
    }

    public class Variant
    {
        public VariantOption Option1 { get; set; } = new VariantOption(){
            Name = "Title",
            Value = "Default Title"
        };

        public string Handle { get; set; } = string.Empty;

        public VariantOption Option2 { get; set; } = new VariantOption();

        public VariantOption Option3 { get; set; } = new VariantOption();

        public string SKU { get; set; }

        public decimal? Grams { get; set; }

        /// <summary>
        /// Leave empty if not tracking otherwise
        /// shopify, shipwire, amazon_marketplace_web
        /// </summary>
        public string InventoryTracker { get; set; } = InventoryTrackers.Shopify;

        public int InventoryQuantity { get; set; }

        /// <summary>
        /// Need to be deny or continue
        /// </summary>
        public string InventoryPolicy { get; set; } = InventoryPolicies.Deny;

        /// <summary>
        /// manual, shipwire, webgistix, amazon_marketplace_web, or custom value
        /// </summary>
        public string FulfillmentService { get; set; } = "manual";

        public decimal? Price { get; set; }

        public decimal? CompareAtPrice { get; set; }

        /// <summary>
        /// Blank also equals false
        /// </summary>
        public bool RequiresShipping { get; set; }

        public bool Taxable { get; set; }

        public string Barcode { get; set; }

        public string Image { get; set; }

        /// <summary>
        /// g, kg, lb, or oz
        /// </summary>
        public string WeightUnit { get; set; } = WeigthUnits.Pound;

        public string TaxCode { get; set; }

        public decimal? CostPerItem { get; set; }
    }
}

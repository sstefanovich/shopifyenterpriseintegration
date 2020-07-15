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
        public static string NotTracked = string.Empty;
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

    public static class FufillmentServices
    {
        public static string Manual = "manual";
        public static string ShipWire = "shipwire";
        public static string Webgistix = "webgistix";
        public static string Amazon = "amazon_marketplace_web";
    }

    /// <summary>
    /// Used to generate a single variant CSV record
    /// </summary>
    public class Variant
    {
        public List<LocationInventory> LocationInventories { get; set; } = new List<LocationInventory>();

        public VariantOption Option1 { get; set; } = new VariantOption(){
            Name = "Title",
            Value = "Default Title"
        };

        /// <summary>
        /// Uniquely identifies a Product and its variants, required
        /// </summary>
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

        public int? InventoryQuantity { get; set; }

        /// <summary>
        /// Needs to be deny or continue
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
        public bool? RequiresShipping { get; set; }

        public bool? Taxable { get; set; }

        public string Barcode { get; set; }

        /// <summary>
        /// Use Image to assign an image directly to a specific variant. For shared images, use the Images collection on the product
        /// Shopify will download the image to their CDN during import
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// g, kg, lb, or oz
        /// </summary>
        public string WeightUnit { get; set; } = WeigthUnits.Pound;

        public string TaxCode { get; set; }

        public decimal? CostPerItem { get; set; }
    }
}

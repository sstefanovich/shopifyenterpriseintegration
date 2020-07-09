using CsvHelper.Configuration.Attributes;
using System;

namespace RDA.Shopify.CSVImport
{
    /// <summary>
    /// Used to generate a single record for a product import CSV when all inventory is at a single location
    /// </summary>
    public class SingleLocationProductRecord
    {
        [Name("Handle")]
        public string Handle { get; set; } = string.Empty;

        [Name("Title")]
        public string Title { get; set; }

        [Name("Body (HTML)")]
        public string Body { get; set; }

        [Name("Vendor")]
        public string Vendor { get; set; }

        [Name("Type")]
        public string Type { get; set; }

        /// <summary>
        /// Comma delimited list, can be blank
        /// </summary>
        [Name("Tags")]
        public string Tags { get; set; }

        [Name("Published")]
        public bool Published { get; set; } = true;

        [Name("Option1 Name")]
        public string Option1Name { get; set; } = "Title";

        [Name("Option1 Value")]
        public string Option1Value { get; set; } = "Default Title";

        [Name("Option2 Name")]
        public string Option2Name { get; set; }

        [Name("Option2 Value")]
        public string Option2Value { get; set; }

        [Name("Option3 Name")]
        public string Option3Name { get; set; }

        [Name("Option3 Value")]
        public string Option3Value { get; set; }

        [Name("Variant SKU")]
        public string VariantSKU { get; set; }

        [Name("Variant Grams")]
        public decimal? VariantGrams { get; set; }

        /// <summary>
        /// Leave empty if not tracking otherwise
        /// shopify, shipwire, amazon_marketplace_web
        /// </summary>
        [Name("Variant Inventory Tracker")]
        public string VariantInventoryTracker { get; set; } = "shopify";

        /// <summary>
        /// Only for single location, remvoe if supporting multiple locations
        /// </summary>
        [Name("Variant Inventory Qty")]
        public int VariantInventoryQty { get; set; }

        /// <summary>
        /// Need to be deny or continue
        /// </summary>
        [Name("Variant Inventory Policy")]
        public string VariantInventoryPolicy { get; set; }

        /// <summary>
        /// manaul, shipwire, webgistix, amazon_marketplace_web
        /// </summary>
        [Name("Variant Fulfillment Service")]
        public string VariantFulfillmentService { get; set; } = "manual";

        [Name("Variant Price")]
        public decimal? VariantPrice { get; set; }

        [Name("Variant Compare At Price")]
        public decimal? VariantCompareAtPrice { get; set; }

        /// <summary>
        /// Blank also equals false
        /// </summary>
        [Name("Variant Requires Shipping")]
        public bool VariantRequiresShipping { get; set; }

        [Name("Variant Taxable")]
        public bool VariantTaxable { get; set; }

        [Name("Variant Barcode")]
        public string VariantBarcode { get; set; }

        [Name("Image Src")]
        public string ImageSrc { get; set; }

        [Name("Image Position")]
        public int? ImagePosition { get; set; }

        [Name("Image Alt Text")]
        public string ImageAltText { get; set; }

        [Name("Gift Card")]
        public bool GiftCard { get; set; } = false;

        [Name("SEO Title")]
        public string SEOTitle { get; set; }

        [Name("SEO Description")]
        public string SEODescription { get; set; }

        [Name("Variant Image")]
        public string VariantImage { get; set; }

        [Name("Google Shopping / Google Product Category")]
        public string GoogleShoppingGoogleProductCategory { get; set; }

        [Name("Google Shopping / Gender")]
        public string GoogleShoppingGender { get; set; }

        [Name("Google Shopping / Age Group")]
        public string GoogleShoppingAgeGroup { get; set; }

        [Name("Google Shopping / MPN")]
        public string GoogleShoppingMPN { get; set; }

        [Name("Google Shopping / AdWords Grouping")]
        public string GoogleShoppingAdWordsGrouping { get; set; }

        [Name("Google Shopping / AdWords Labels")]
        public string GoogleShoppingAdWordsLabels { get; set; }

        [Name("Google Shopping / Condition")]
        public string GoogleShoppingCondition { get; set; }

        [Name("Google Shopping / Custom Product")]
        public string GoogleShoppingCustomProduct { get; set; }

        [Name("Google Shopping / Custom Label 0")]
        public string GoogleShoppingCustomLabel0 { get; set; }

        [Name("Google Shopping / Custom Label 1")]
        public string GoogleShoppingCustomLabel1 { get; set; }

        [Name("Google Shopping / Custom Label 2")]
        public string GoogleShoppingCustomLabel2 { get; set; }

        [Name("Google Shopping / Custom Label 3")]
        public string GoogleShoppingCustomLabel3 { get; set; }

        [Name("Google Shopping / Custom Label 4")]
        public string GoogleShoppingCustomLabel4 { get; set; }

        /// <summary>
        /// g, kg, lb, or oz
        /// </summary>
        [Name("Variant Weight Unit")]
        public string VariantWeightUnit { get; set; } = "lb";

        [Name("Cost per item")]
        public decimal? CostPerItem { get; set; }
    }
}

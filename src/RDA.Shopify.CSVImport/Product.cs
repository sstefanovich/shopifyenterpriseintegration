using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;

namespace RDA.Shopify.CSVImport
{
    /// <summary>
    /// Used to generate a single record for a product import CSV 
    /// </summary>
    public class Product : Variant
    {
        public List<Variant> Variants { get; internal set; } = new List<Variant>();

        public List<Image> Images { get; internal set; } = new List<Image>();

        /// <summary>
        /// Comma delimited list, can be blank
        /// </summary>
        public List<string> Tags { get; internal set; } = new List<string>();

        public string Title { get; set; }

        public string Body { get; set; }

        public string Vendor { get; set; }

        public string Type { get; set; }

        public bool Published { get; set; } = true;

        public bool GiftCard { get; set; } = false;

        public string SEOTitle { get; set; }

        public string SEODescription { get; set; }

        public string GoogleShoppingGoogleProductCategory { get; set; }

        public string GoogleShoppingGender { get; set; }

        public string GoogleShoppingAgeGroup { get; set; }

        public string GoogleShoppingMPN { get; set; }

        public string GoogleShoppingAdWordsGrouping { get; set; }

        public string GoogleShoppingAdWordsLabels { get; set; }

        public string GoogleShoppingCondition { get; set; }

        public string GoogleShoppingCustomProduct { get; set; }

        public string GoogleShoppingCustomLabel0 { get; set; }

        public string GoogleShoppingCustomLabel1 { get; set; }

        public string GoogleShoppingCustomLabel2 { get; set; }

        public string GoogleShoppingCustomLabel3 { get; set; }

        public string GoogleShoppingCustomLabel4 { get; set; }
    }
}

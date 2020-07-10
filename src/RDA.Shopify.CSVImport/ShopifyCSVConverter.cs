using System;
using System.Collections.Generic;
using System.Text;

namespace RDA.Shopify.CSVImport
{
    public class ShopifyCSVConverter : ICSVConverter
    {
        public string Convert(ProductInventory inventory)
        {
            var csvResult = new StringBuilder();

            //Add the headers first, they must appear in this order
            csvResult.AppendLine(@"Handle,Title,Body (HTML),Vendor,Type,Tags,Published,Option1 Name,Option1 Value,Option2 Name,Option2 Value,Option3 Name,Option3 Value,Variant SKU,Variant Grams,Variant Inventory Tracker,Variant Inventory Qty,Variant Inventory Policy,Variant Fulfillment Service,Variant Price,Variant Compare At Price,Variant Requires Shipping,Variant Taxable,Variant Barcode,Image Src,Image Position,Image Alt Text,Gift Card,SEO Title,SEO Description,Google Shopping / Google Product Category,Google Shopping / Gender,Google Shopping / Age Group,Google Shopping / MPN,Google Shopping / AdWords Grouping,Google Shopping / AdWords Labels,Google Shopping / Condition,Google Shopping / Custom Product,Google Shopping / Custom Label 0,Google Shopping / Custom Label 1,Google Shopping / Custom Label 2,Google Shopping / Custom Label 3,Google Shopping / Custom Label 4,Variant Image,Variant Weight Unit,Variant Tax Code,Cost per item");

            foreach(var product in inventory.Products)
            {
                csvResult.AppendLine(BuildProduct(product));

                for (int imageIndex = 1; imageIndex < product.Images.Count; imageIndex++)
                {
                    csvResult.AppendLine(BuildImage(product, product.Images[imageIndex], imageIndex + 1));
                }

                foreach(var variant in product.Variants)
                {
                    csvResult.AppendLine(BuildVariant(product, variant));
                }
            }

            return csvResult.ToString();
        }

        private string BuildProduct(Product product)
        {
            var productRecord = new StringBuilder();

            productRecord.Append(product.Handle);
            productRecord.Append(",");

            productRecord.Append(product.Title);
            productRecord.Append(",");

            productRecord.AppendQuoted(product.Body);
            productRecord.Append(",");

            productRecord.Append(product.Vendor);
            productRecord.Append(",");

            productRecord.Append(product.Type);
            productRecord.Append(",");

            productRecord.AppendQuoted(product.Tags.Count > 0 ? string.Join(",", product.Tags) : string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.Published.ToString().ToUpper());
            productRecord.Append(",");

            productRecord.Append(product.Option1.Name);
            productRecord.Append(",");

            productRecord.Append(product.Option1.Value);
            productRecord.Append(",");

            productRecord.Append(product.Option2.Name ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.Option2.Value ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.Option3.Name ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.Option3.Value ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.SKU ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.Grams?.ToString() ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.InventoryTracker);
            productRecord.Append(",");

            productRecord.Append(product.InventoryQuantity.ToString());
            productRecord.Append(",");

            productRecord.Append(product.InventoryPolicy);
            productRecord.Append(",");

            productRecord.Append(product.FulfillmentService ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.Price?.ToString() ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.CompareAtPrice?.ToString() ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.RequiresShipping.ToString().ToUpper());
            productRecord.Append(",");

            productRecord.Append(product.Taxable.ToString().ToUpper());
            productRecord.Append(",");

            productRecord.Append(product.Barcode ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.Images.Count > 0 ? product.Images[0].Source : string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.Images.Count > 0 ? "1" : string.Empty);
            productRecord.Append(",");

            productRecord.AppendQuoted(product.Images.Count > 0 ? product.Images[0].AltText : string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.GiftCard.ToString().ToUpper());
            productRecord.Append(",");

            productRecord.Append(product.SEOTitle ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.SEODescription ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.GoogleShoppingGoogleProductCategory ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.GoogleShoppingGender ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.GoogleShoppingAgeGroup ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.GoogleShoppingMPN ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.GoogleShoppingAdWordsGrouping ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.GoogleShoppingAdWordsLabels ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.GoogleShoppingCondition ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.GoogleShoppingCustomProduct ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.GoogleShoppingCustomLabel0 ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.GoogleShoppingCustomLabel1 ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.GoogleShoppingCustomLabel2 ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.GoogleShoppingCustomLabel3 ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.GoogleShoppingCustomLabel4 ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.Image ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.WeightUnit);
            productRecord.Append(",");

            productRecord.Append(product.TaxCode ?? string.Empty);
            productRecord.Append(",");

            productRecord.Append(product.CostPerItem?.ToString() ?? string.Empty);

            return productRecord.ToString();
        }

        private string BuildImage(Product product, Image image, int position)
        {
            var imageRecord = new StringBuilder();

            imageRecord.Append(product.Handle);
            imageRecord.Append(",,,,,,,,,,,,,,,,,,,,,,,");


            imageRecord.Append(image.Source);
            imageRecord.Append(",");

            imageRecord.Append(position.ToString());
            imageRecord.Append(",");

            imageRecord.Append(image.AltText ?? string.Empty);
            imageRecord.Append(",,,,,,,,,,,,,,,,,,,,");

            

            return imageRecord.ToString();
        }

        private string BuildVariant(Product product, Variant variant)
        {
            var variantRecord = new StringBuilder();

            variantRecord.Append(product.Handle);
            variantRecord.Append(",,,,,,,");


            variantRecord.Append(product.Option1.Name);
            variantRecord.Append(",");

            variantRecord.Append(product.Option1.Value);
            variantRecord.Append(",");

            variantRecord.Append(product.Option2.Name ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(product.Option2.Value ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(product.Option3.Name ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(product.Option3.Value ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(product.SKU ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(product.Grams?.ToString() ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(product.InventoryTracker);
            variantRecord.Append(",");

            variantRecord.Append(product.InventoryQuantity.ToString());
            variantRecord.Append(",");

            variantRecord.Append(product.InventoryPolicy);
            variantRecord.Append(",");

            variantRecord.Append(product.FulfillmentService ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(product.Price?.ToString() ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(product.CompareAtPrice?.ToString() ?? string.Empty);
            variantRecord.Append(",");

            //TODO - Remove if using multiple locations
            variantRecord.Append(product.InventoryQuantity);
            variantRecord.Append(",");

            variantRecord.Append(product.RequiresShipping.ToString().ToUpper());
            variantRecord.Append(",");


            variantRecord.Append(product.Taxable.ToString().ToUpper());
            variantRecord.Append(",");

            variantRecord.Append(product.Barcode ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(",,,");


            variantRecord.Append(",,,,,,,,,,,,,,,,");
          

            variantRecord.Append(product.Image ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(product.WeightUnit);
            variantRecord.Append(",");

            variantRecord.Append(product.TaxCode ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(product.CostPerItem?.ToString() ?? string.Empty);

            return variantRecord.ToString();
        }
    }
}

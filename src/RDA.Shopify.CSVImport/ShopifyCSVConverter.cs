using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace RDA.Shopify.CSVImport
{
    public class ShopifyCSVConverter : ICSVConverter
    {
        public (string products, string inventory) Convert(ProductInventory inventory)
        {
            var productResult = new StringBuilder();
            var inventoryResult = new StringBuilder();
            var locationNames = new List<string>();

            //Add the headers first, they must appear in this order
            productResult.AppendLine(@"Handle,Title,Body (HTML),Vendor,Type,Tags,Published,Option1 Name,Option1 Value,Option2 Name,Option2 Value,Option3 Name,Option3 Value,Variant SKU,Variant Grams,Variant Inventory Tracker,Variant Inventory Qty,Variant Inventory Policy,Variant Fulfillment Service,Variant Price,Variant Compare At Price,Variant Requires Shipping,Variant Taxable,Variant Barcode,Image Src,Image Position,Image Alt Text,Gift Card,SEO Title,SEO Description,Google Shopping / Google Product Category,Google Shopping / Gender,Google Shopping / Age Group,Google Shopping / MPN,Google Shopping / AdWords Grouping,Google Shopping / AdWords Labels,Google Shopping / Condition,Google Shopping / Custom Product,Google Shopping / Custom Label 0,Google Shopping / Custom Label 1,Google Shopping / Custom Label 2,Google Shopping / Custom Label 3,Google Shopping / Custom Label 4,Variant Image,Variant Weight Unit,Variant Tax Code,Cost per item");

            //If any product or variant has inventory locations, then they all should
            bool trackLocationInventory = inventory.Products.Where(p => p.LocationInventories.Count > 0).Any() || inventory.Products.Where(p => p.Variants.Where(v => v.LocationInventories.Count > 0).Count() > 0).Any();

            foreach (var product in inventory.Products)
            {
                productResult.AppendLine(BuildProduct(product, trackLocationInventory));

                foreach (var locationInventory in product.LocationInventories)
                {
                    if (!locationNames.Contains(locationInventory.LocationName))
                        locationNames.Add(locationInventory.LocationName);
                }

                //The first image goes on the main product row then each additional gets a row
                for (int imageIndex = 1; imageIndex < product.Images.Count; imageIndex++)
                {
                    productResult.AppendLine(BuildImage(product, product.Images[imageIndex], imageIndex + 1));
                }

                foreach (var variant in product.Variants)
                {
                    foreach (var locationInventory in product.LocationInventories)
                    {
                        if (!locationNames.Contains(locationInventory.LocationName))
                            locationNames.Add(locationInventory.LocationName);
                    }

                    productResult.AppendLine(BuildVariant(product, variant, trackLocationInventory));
                }
            }

            if (trackLocationInventory && locationNames.Count > 0)
            {
                //We need to get all the location names, each becomes a column
                inventoryResult.AppendLine($@"Handle,Title,Option1 Name,Option1 Value,Option2 Name,Option2 Value,Option3 Name,Option3 Value{string.Join(",", locationNames)}");

                foreach (var product in inventory.Products)
                {
                    inventoryResult.AppendLine(BuildInventory(product, null, locationNames));

                    foreach (var variant in product.Variants)
                    {
                       inventoryResult.AppendLine(BuildInventory(product, variant, locationNames));
                    }
                }
            }

            return (productResult.ToString(), inventoryResult.ToString());
        }

        private string BuildInventory(Product product, Variant variant, List<string> locationNames)
        {
            var inventoryRecord = new StringBuilder();
            List<LocationInventory> inventories = null;

            inventoryRecord.Append(product.Handle);
            inventoryRecord.Append(", ");

            inventoryRecord.Append(product.Title);
            inventoryRecord.Append(",");

            if (variant == null)
            {
                inventories = product.LocationInventories;

                inventoryRecord.Append(product.Option1.Name);
                inventoryRecord.Append(",");

                inventoryRecord.Append(product.Option1.Value);
                inventoryRecord.Append(",");

                inventoryRecord.Append(product.Option2.Name ?? string.Empty);
                inventoryRecord.Append(",");

                inventoryRecord.Append(product.Option2.Value ?? string.Empty);
                inventoryRecord.Append(",");

                inventoryRecord.Append(product.Option3.Name ?? string.Empty);
                inventoryRecord.Append(",");

                inventoryRecord.Append(product.Option3.Value ?? string.Empty);
                inventoryRecord.Append(",");

                inventoryRecord.Append(product.SKU ?? string.Empty);
                inventoryRecord.Append(",");
            }
            else
            {
                inventories = variant.LocationInventories;

                inventoryRecord.Append(variant.Option1.Name);
                inventoryRecord.Append(",");

                inventoryRecord.Append(variant.Option1.Value);
                inventoryRecord.Append(",");

                inventoryRecord.Append(variant.Option2.Name ?? string.Empty);
                inventoryRecord.Append(",");

                inventoryRecord.Append(variant.Option2.Value ?? string.Empty);
                inventoryRecord.Append(",");

                inventoryRecord.Append(variant.Option3.Name ?? string.Empty);
                inventoryRecord.Append(",");

                inventoryRecord.Append(variant.Option3.Value ?? string.Empty);
                inventoryRecord.Append(",");

                inventoryRecord.Append(variant.SKU ?? string.Empty);
                
            }

            foreach(var locationName in locationNames)
            {
                var inventory = inventories.Where(i => i.LocationName.Equals(locationName)).FirstOrDefault();

                inventoryRecord.Append(",");

                if (inventory == null || ! inventory.IsStockedAtLocation)
                {
                    inventoryRecord.Append("Not stocked");
                }
                else
                {
                    inventoryRecord.Append(inventory.InventoryAmount.ToString());
                }

            }

            return inventoryRecord.ToString();
        }

        private string BuildProduct(Product product, bool trackLocationInventory)
        {
            var productRecord = new StringBuilder();

            productRecord.Append(product.Handle);
            productRecord.Append(", ");

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

            if (!trackLocationInventory)
            {
                productRecord.Append(product.InventoryQuantity.ToString());
                productRecord.Append(",");
            }

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
            imageRecord.Append(",,,,,,,,,,,,,,,,,,,,,,,,");


            imageRecord.Append(image.Source);
            imageRecord.Append(",");

            imageRecord.Append(position.ToString());
            imageRecord.Append(",");

            imageRecord.Append(image.AltText ?? string.Empty);
            imageRecord.Append(",,,,,,,,,,,,,,,,,,,,");

            return imageRecord.ToString();
        }

        private string BuildVariant(Product product, Variant variant, bool trackLocationInventory)
        {
            var variantRecord = new StringBuilder();

            variantRecord.Append(product.Handle);
            variantRecord.Append(",,,,,,,");

            variantRecord.Append(string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(variant.Option1.Value);
            variantRecord.Append(",");

            variantRecord.Append(string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(variant.Option2.Value ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(variant.Option3.Value ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(variant.SKU ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(variant.Grams?.ToString() ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(variant.InventoryTracker ?? string.Empty);
            variantRecord.Append(",");

            if (!trackLocationInventory)
            {
                variantRecord.Append(variant.InventoryQuantity.ToString());
                variantRecord.Append(",");
            }

            variantRecord.Append(variant.InventoryPolicy ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(variant.FulfillmentService ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(variant.Price?.ToString() ?? "0");
            variantRecord.Append(",");

            variantRecord.Append(variant.CompareAtPrice?.ToString() ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(variant.RequiresShipping.ToString().ToUpper());
            variantRecord.Append(",");

            variantRecord.Append(variant.Taxable.ToString().ToUpper());
            variantRecord.Append(",");

            variantRecord.Append(variant.Barcode ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(",,,,,,,,,,,,,,,,,,,");

            variantRecord.Append(variant.Image ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(variant.WeightUnit ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(variant.TaxCode ?? string.Empty);
            variantRecord.Append(",");

            variantRecord.Append(variant.CostPerItem?.ToString() ?? string.Empty);

            return variantRecord.ToString();
        }
    }
}

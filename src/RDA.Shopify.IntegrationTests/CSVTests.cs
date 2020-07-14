using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDA.Shopify.CSVImport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RDA.Shopify.IntegrationTests
{
    [TestClass]
    public class CSVTests
    {
        [TestMethod]
        public void CSV_Build_Test()
        {
            var inventory = new ProductInventory();
            var converter = new ShopifyCSVConverter();

            inventory.AddProduct(new Product() { 
                Body = "Body Text",
                FulfillmentService = "manual",
                GiftCard = false,
                Grams = 50,
                Handle = "123",
                InventoryPolicy = InventoryPolicies.Deny,
                InventoryQuantity = 50,
                InventoryTracker = InventoryTrackers.Shopify,
                Option1 = new VariantOption(){ 
                    Name = "Size",
                    Value = "Small"
                },
                Price = 19.99m,
                Published = true,
                RequiresShipping = true,
                SKU = "2345",
                Taxable = true,
                Title = "Title",
                Type = "Type",
                Vendor = "RDA",
                WeightUnit = WeigthUnits.Pound
            })
            .AddImage(new Image(){ 
                AltText = "Alt Text Image 1",
                Source = @"https://storageaccountshopia6d8.blob.core.windows.net/artwork/10022.jpg"
            })
            .AddImage(new Image()
            {
                AltText = "Alt Text Image 2",
                Source = @"https://storageaccountshopia6d8.blob.core.windows.net/artwork/10281.jpg"
            })
            .AddTag("Tag1").AddTag("Tag2")
            .AddVariant(new Variant() {
                Option1 = new VariantOption()
                {
                    Name = "Size",
                    Value = "Medium",
                }
            }, true)
            .AddVariant(new Variant()
            {
                Image = "https://storageaccountshopia6d8.blob.core.windows.net/artwork/10958.jpg",
                Option1 = new VariantOption()
                {
                    Name = "Size",
                    Value = "Large",
                }
            }, true);

            (string productCsv, string inventoryCsv) = converter.Convert(inventory);

            Assert.IsTrue(productCsv != null);

            File.WriteAllText(@"C:\Temp\Test.csv", productCsv, Encoding.UTF8);
        }
    }
}

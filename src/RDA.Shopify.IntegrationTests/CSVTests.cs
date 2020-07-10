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
                Published = false,
                RequiresShipping = true,
                SKU = "2345",
                Taxable = true,
                Title = "Title",
                Type = "Type",
                Vendor = "RDA",
                WeightUnit = WeigthUnits.Pound
            }).AddImage(new Image(){ 
                AltText = "Alt Text",
                Source = @"http://www.foo.com/img1.jpf"
            }).AddTag("Tag1").AddTag("Tag2").AddVariant(new Variant() {
                Option1 = new VariantOption()
                {
                    Name = "Size",
                    Value = "Medium",
                },
                InventoryQuantity = 22
            }).AddVariant(new Variant()
            {
                Option1 = new VariantOption()
                {
                    Name = "Size",
                    Value = "Large",
                },
                InventoryQuantity = 25
            });

            var result = converter.Convert(inventory);

            Assert.IsTrue(result != null);

            File.WriteAllText(@"C:\Temp\Test.csv", result, Encoding.UTF8);
        }
    }
}

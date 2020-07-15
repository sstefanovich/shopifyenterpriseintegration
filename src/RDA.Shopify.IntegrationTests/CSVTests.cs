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
        public void CSV_Build_Variants_Single_Location()
        {
            var inventory = new ProductInventory();
            var converter = new ShopifyCSVConverter();

            inventory.AddProduct(new Product() { 
                Body = "Body Text",
                FulfillmentService = FufillmentServices.Manual,
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

            //No inventory
            Assert.IsTrue(string.IsNullOrEmpty(inventoryCsv));

            //Header + 1 product + 1 image + 2 variants = 5 lines
            Assert.IsTrue(productCsv.Split('\n').Length == 5);

            //File.WriteAllText(@"C:\Temp\Test-Variants.csv", productCsv, Encoding.UTF8);
        }

        [TestMethod]
        public void CSV_Build_Variants__Multiple_Locations()
        {
            var inventory = new ProductInventory();
            var converter = new ShopifyCSVConverter();

            inventory.AddProduct(new Product()
            {
                Body = "Body Text",
                FulfillmentService = FufillmentServices.Manual,
                GiftCard = false,
                Grams = 50,
                Handle = "123",
                InventoryPolicy = InventoryPolicies.Deny,
                InventoryQuantity = 50,
                InventoryTracker = InventoryTrackers.Shopify,
                Option1 = new VariantOption()
                {
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
            .AddImage(new Image()
            {
                AltText = "Alt Text Image 1",
                Source = @"https://storageaccountshopia6d8.blob.core.windows.net/artwork/10022.jpg"
            })
            .AddImage(new Image()
            {
                AltText = "Alt Text Image 2",
                Source = @"https://storageaccountshopia6d8.blob.core.windows.net/artwork/10281.jpg"
            })
            .AddTag("Tag1").AddTag("Tag2")
            .AddVariant(new Variant()
            {
                Option1 = new VariantOption()
                {
                    Name = "Size",
                    Value = "Medium",
                },
                LocationInventories = new List<LocationInventory>()
                {
                    new LocationInventory(){InventoryAmount = 10, IsStockedAtLocation = true, LocationName = "Home"},
                    new LocationInventory(){InventoryAmount = 10, IsStockedAtLocation = true, LocationName = "Location1"}
                }
            }, true)
            .AddVariant(new Variant()
            {
                Image = "https://storageaccountshopia6d8.blob.core.windows.net/artwork/10958.jpg",
                Option1 = new VariantOption()
                {
                    Name = "Size",
                    Value = "Large",
                },
                LocationInventories = new List<LocationInventory>()
                {
                    new LocationInventory(){InventoryAmount = 10, IsStockedAtLocation = true, LocationName = "Home"},
                    new LocationInventory(){IsStockedAtLocation = false, LocationName = "Location1"}
                }
            }, true);

            (string productCsv, string inventoryCsv) = converter.Convert(inventory);

            Assert.IsTrue(productCsv != null);

            //No inventory
            Assert.IsTrue(inventoryCsv != null);

            //Header + 1 product + 1 image + 2 variants = 5 lines
            Assert.IsTrue(productCsv.Split('\n').Length == 5);

            File.WriteAllText(@"C:\Temp\Test-Variants.csv", productCsv, Encoding.UTF8);
            File.WriteAllText(@"C:\Temp\Test-Inventory.csv", inventoryCsv, Encoding.UTF8);
        }

        [TestMethod]
        public void CSV_Build_No_Variants_Single_Location()
        {
            var inventory = new ProductInventory();
            var converter = new ShopifyCSVConverter();

            inventory.AddProduct(new Product()
            {
                Body = "Body Text",
                FulfillmentService = FufillmentServices.Manual,
                GiftCard = false,
                Grams = 50,
                Handle = "123",
                InventoryPolicy = InventoryPolicies.Deny,
                InventoryQuantity = 50,
                InventoryTracker = InventoryTrackers.Shopify,
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
            .AddImage(new Image()
            {
                AltText = "Alt Text Image 1",
                Source = @"https://storageaccountshopia6d8.blob.core.windows.net/artwork/10022.jpg"
            })
            .AddImage(new Image()
            {
                AltText = "Alt Text Image 2",
                Source = @"https://storageaccountshopia6d8.blob.core.windows.net/artwork/10281.jpg"
            })
            .AddTag("Tag1").AddTag("Tag2");

            (string productCsv, string inventoryCsv) = converter.Convert(inventory);

            Assert.IsTrue(productCsv != null);

            //No inventory
            Assert.IsTrue(string.IsNullOrEmpty(inventoryCsv));

            //Header + 1 product + 1 image = 3 lines
            Assert.IsTrue(productCsv.Split('\n').Length == 3);

            //File.WriteAllText(@"C:\Temp\Test-No-Variants.csv", productCsv, Encoding.UTF8);
        }

        [TestMethod]
        public void CSV_Build_No_Variants_No_Inventory_Tracking()
        {
            var inventory = new ProductInventory();
            var converter = new ShopifyCSVConverter();

            inventory.AddProduct(new Product()
            {
                Body = "Body Text",
                FulfillmentService = FufillmentServices.Manual,
                GiftCard = false,
                Grams = 50,
                Handle = "123",
                InventoryPolicy = InventoryPolicies.Deny,
                InventoryQuantity = 50,
                InventoryTracker = InventoryTrackers.NotTracked,
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
            .AddImage(new Image()
            {
                AltText = "Alt Text Image 1",
                Source = @"https://storageaccountshopia6d8.blob.core.windows.net/artwork/10022.jpg"
            })
            .AddImage(new Image()
            {
                AltText = "Alt Text Image 2",
                Source = @"https://storageaccountshopia6d8.blob.core.windows.net/artwork/10281.jpg"
            })
            .AddTag("Tag1").AddTag("Tag2");

            (string productCsv, string inventoryCsv) = converter.Convert(inventory);

            Assert.IsTrue(productCsv != null);

            //No inventory
            Assert.IsTrue(string.IsNullOrEmpty(inventoryCsv));

            //Header + 1 product + 1 image = 3 lines
            Assert.IsTrue(productCsv.Split('\n').Length == 3);

            File.WriteAllText(@"C:\Temp\Test-No-Variants-no-inventory-tracking.csv", productCsv, Encoding.UTF8);
        }

        [TestMethod]
        public void CSV_Build_No_Variants_Multiple_Locations()
        {
            var inventory = new ProductInventory();
            var converter = new ShopifyCSVConverter();

            inventory.AddProduct(new Product()
            {
                Body = "Body Text",
                FulfillmentService = FufillmentServices.Manual,
                GiftCard = false,
                Grams = 50,
                Handle = "123",
                InventoryPolicy = InventoryPolicies.Deny,
                InventoryQuantity = 50,
                InventoryTracker = InventoryTrackers.Shopify,
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
            .AddImage(new Image()
            {
                AltText = "Alt Text Image 1",
                Source = @"https://storageaccountshopia6d8.blob.core.windows.net/artwork/10022.jpg"
            })
            .AddImage(new Image()
            {
                AltText = "Alt Text Image 2",
                Source = @"https://storageaccountshopia6d8.blob.core.windows.net/artwork/10281.jpg"
            })
            .AddTag("Tag1").AddTag("Tag2")
            .AddInventory(new LocationInventory() { 
                InventoryAmount = 53,
                IsStockedAtLocation = true,
                LocationName = "Location1"
            });

            (string productCsv, string inventoryCsv) = converter.Convert(inventory);

            Assert.IsTrue(productCsv != null);

            //Header + 1 product + 1 image = 3 lines
            Assert.IsTrue(productCsv.Split('\n').Length == 3);

            Assert.IsTrue(inventoryCsv.Split('\n').Length == 2);

        }
    }
}

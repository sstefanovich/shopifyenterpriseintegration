using System;
using System.Collections.Generic;
using System.Text;

namespace RDA.Shopify.CSVImport
{
    public static class VariantExtensions
    {
        public static Variant AddInventory(this Variant variant, LocationInventory inventory)
        {
            variant.LocationInventories.Add(inventory);

            return variant;
        }
    }
}

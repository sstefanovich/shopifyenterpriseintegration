using System;
using System.Collections.Generic;
using System.Text;

namespace RDA.Shopify.CSVImport
{
    public sealed class LocationInventory
    {
        public string LocationName { get; set; }

        public int InventoryAmount { get; set; }

        public bool IsStockedAtLocation { get; set; }
    }
}

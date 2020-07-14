using System;
using System.Collections.Generic;
using System.Text;

namespace RDA.Shopify.CSVImport
{
    interface ICSVConverter
    {
        (string products, string inventory) Convert(ProductInventory inventory);
    }
}

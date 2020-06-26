using System;
using System.Collections.Generic;
using System.Text;

namespace RDA.Shopify.Entities
{
    public class Edge<TValue>
    {
        public string cursor { get; set; }

        public TValue node { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RDA.Shopify.Entities
{

    public class CollectionResult<TValue>
    {

        public List<Edge<TValue>> edges { get; set; }


        public PageInfo pageInfo { get; set; }

    }

}

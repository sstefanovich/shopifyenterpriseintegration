using System;
using System.Collections.Generic;
using System.Text;

namespace RDA.Shopify.Entities
{
    public class DraftOrderCreateResponse
    {
        public DraftOrderCreate DraftOrderCreate { get; set; }
    }


    public class DraftOrderCreate
    {
        public DraftOrder DraftOrder { get; set; }

        public List<UserError> UserErrors { get; set; } = new List<UserError>();
    }
}

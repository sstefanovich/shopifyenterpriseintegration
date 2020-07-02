using System;
using System.Collections.Generic;
using System.Text;

namespace RDA.Shopify.Entities
{
    public class DraftOrderInvoiceSendResponse
    {
        public DraftOrderInvoiceSend DraftOrderInvoiceSend { get; set; }
    }

    public class DraftOrderInvoiceSend
    {
        public List<UserError> UserErrors { get; set; } = new List<UserError>();

        public DraftOrder DraftOrder { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RDA.Shopify.Entities
{
    public class WebHookSubscriptionDelete
    {
        public string DeletedWebhookSubscriptionId { get; set; }

        public List<UserError> UserErrors { get; set; } = new List<UserError>();
    }
}

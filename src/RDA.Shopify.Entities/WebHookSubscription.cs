using System;
using System.Collections.Generic;
using System.Text;

namespace RDA.Shopify.Entities
{
    public class WebHookSubscriptions
    {
        public CollectionResult<WebHookSubscription> WebhookSubscriptions { get; set; }
    }
    
    public class WebHookSubscription
    {
        public DateTime CreatedAt { get; set; }

        public string CallbackUrl { get; set; }

        public string Format { get; set; }

        public string Id { get; set; }

        public List<string> IncludedFields { get; set; } = new List<string>();

        public string Topic { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string LegacyResourceId { get; set; }

        public List<string> MetaFieldNamespaces { get; set; }
    }
}

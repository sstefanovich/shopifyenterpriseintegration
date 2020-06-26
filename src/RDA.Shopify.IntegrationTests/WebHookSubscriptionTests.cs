using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDA.Shopify.Communication;
using RDA.Shopify.Entities;
using System.Threading.Tasks;

namespace RDA.Shopify.IntegrationTests
{
    [TestClass]
    public class WebHookSubscriptionTests
    {
        [TestMethod]
        public async Task WebhookSubscriptions_Load()
        {
            var shopifyRepo = new ShopifyAdminRepository();
            var query = @"{webhookSubscriptions(first:100)
                {
                    pageInfo { hasNextPage hasPreviousPage}
                    edges{
                        cursor
                        node { createdAt format callbackUrl id includeFields topic updatedAt legacyResourceId metafieldNamespaces}
                    }
                }
            }";

            var result = await shopifyRepo.ExecuteQuery<WebHookSubscriptions>(query);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task WebhookSubscriptions_Delete()
        {
            var shopifyRepo = new ShopifyAdminRepository();
            var query = @"mutation webhookSubscriptionDelete {
                webhookSubscriptionDelete(id: 'gid:\/\/shopify\/WebhookSubscription\/775024836682') {
                    deletedWebhookSubscriptionId
                    userErrors {
                        field
                        message
                    }
                }
            }";

            var result = await shopifyRepo.ExecuteMutation<WebHookSubscriptionDelete>(query);

            Assert.IsNotNull(result);
        }
    }
}

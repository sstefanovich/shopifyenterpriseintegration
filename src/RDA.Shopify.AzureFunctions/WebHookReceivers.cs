using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Azure.WebJobs.ServiceBus;
using System.Net.Http;
using RDA.Shopify.Communication;

namespace RDA.Shopify.AzureFunctions
{
    public class WebHookReceivers
    {
        private readonly string shopifySharedSecret;
        private readonly string shopifyApiKey;
        private readonly string shopifyApiPassword;

        private readonly IWebHookValidator _validator;
        
        private static readonly string serviceBusConnectionSetting = "ServiceBusConnection";


        public WebHookReceivers(IWebHookValidator validator)
        {
            shopifySharedSecret = Environment.GetEnvironmentVariable("shopifySharedSecret");
            shopifyApiKey = Environment.GetEnvironmentVariable("shopifyApiKey");
            shopifyApiPassword = Environment.GetEnvironmentVariable("shopifyApiPassword");

            _validator = validator;
        }

        [FunctionName("CartCreation")]
        public async Task<IActionResult> CartCreation([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "cartcreation", binder, log);
        }

        [FunctionName("CartUpdate")]
        public async Task<IActionResult> CartUpdate([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "cartupdate", binder, log);
        }

        [FunctionName("CheckoutCreation")]
        public async Task<IActionResult> CheckoutCreation([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "checkoutcreation", binder, log);
        }

        [FunctionName("CheckoutDeletion")]
        public async Task<IActionResult> CheckoutDeletion([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "checkoutdeletion", binder, log);
        }

        [FunctionName("CheckoutUpdate")]
        public async Task<IActionResult> CheckoutUpdate([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "checkoutupdate", binder, log);
        }

        [FunctionName("CollectionCreation")]
        public async Task<IActionResult> CollectionCreation([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "collectioncreation", binder, log);
        }

        [FunctionName("CollectionDeletion")]
        public async Task<IActionResult> CollectionDeletion([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "collectiondeletion", binder, log);
        }

        [FunctionName("CollectionUpdate")]
        public async Task<IActionResult> CollectionUpdate([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "collectionupdate", binder, log);
        }

        [FunctionName("CustomerGroupCreation")]
        public async Task<IActionResult> CustomerGroupCreation([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "customergroupcreation", binder, log);
        }

        [FunctionName("CustomerGroupDeletion")]
        public async Task<IActionResult> CustomerGroupDeletion([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "customergroupdeletion", binder, log);
        }

        [FunctionName("CustomerGroupUpdate")]
        public async Task<IActionResult> CustomerGroupUpdate([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "customergroupupdate", binder, log);
        }

        [FunctionName("CustomerCreation")]
        public async Task<IActionResult> CustomerCreation([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "customercreation", binder, log);
        }

        [FunctionName("CustomerDeletion")]
        public async Task<IActionResult> CustomerDeletion([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "customerdeletion", binder, log);
        }

        [FunctionName("CustomerDisable")]
        public async Task<IActionResult> CustomerDisable([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "customerdisable", binder, log);
        }

        [FunctionName("CustomerEnable")]
        public async Task<IActionResult> CustomerEnable([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "customerenable", binder, log);
        }

        [FunctionName("CustomerUpdate")]
        public async Task<IActionResult> CustomerUpdate([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "customerupdate", binder, log);
        }

        [FunctionName("DraftOrderCreation")]
        public async Task<IActionResult> DraftOrderCreation([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "draftordercreation", binder, log);
        }

        [FunctionName("DraftOrderDeletion")]
        public async Task<IActionResult> DraftOrderDeletion([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "draftorderdeletion", binder, log);
        }

        [FunctionName("DraftOrderUpdate")]
        public async Task<IActionResult> DraftOrderUpdate([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "draftorderupdate", binder, log);
        }

        [FunctionName("FulfillmentCreation")]
        public async Task<IActionResult> FulfillmentCreation([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "fulfillmentcreation", binder, log);
        }

        [FunctionName("FulfillmentUpdate")]
        public async Task<IActionResult> FulfillmentUpdate([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "fulfillmentupdate", binder, log);
        }

        [FunctionName("OrderCancellation")]
        public async Task<IActionResult> OrderCancellation([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "ordercancellation", binder, log);
        }

        [FunctionName("OrderCreation")]
        public async Task<IActionResult> OrderCreation([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "ordercreation", binder, log);
        }

        [FunctionName("OrderDeletion")]
        public async Task<IActionResult> OrderDeletion([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "orderdeletion", binder, log);
        }

        [FunctionName("OrderFulfillment")]
        public async Task<IActionResult> OrderFulfillment([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "orderfulfillment", binder, log);
        }

        [FunctionName("OrderPayment")]
        public async Task<IActionResult> OrderPayment([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "orderpayment", binder, log);
        }

        [FunctionName("OrderUpdate")]
        public async Task<IActionResult> OrderUpdate([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "orderupdate", binder, log);
        }

        [FunctionName("ProductCreation")]
        public async Task<IActionResult> ProductCreation([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "productcreation", binder, log);
        }

        [FunctionName("ProductDeletion")]
        public async Task<IActionResult> ProductDeletion([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "productdeletion", binder, log);
        }

        [FunctionName("ProductUpdate")]
        public async Task<IActionResult> ProductUpdate([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "productupdate", binder, log);
        }

        [FunctionName("RefundCreate")]
        public async Task<IActionResult> RefundCreate([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "refundcreate", binder, log);
        }

        [FunctionName("ShopUpdate")]
        public async Task<IActionResult> ShopUpdate([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "shopupdate", binder, log);
        }

        [FunctionName("ThemeCreation")]
        public async Task<IActionResult> ThemeCreation([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "themecreation", binder, log);
        }

        [FunctionName("ThemeDeletion")]
        public async Task<IActionResult> ThemeDeletion([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "themedeletion", binder, log);
        }

        [FunctionName("ThemeUpdate")]
        public async Task<IActionResult> ThemeUpdate([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, IBinder binder)
        {
            return await ValidateAndRouteMessage(req, "themeupdate", binder, log);
        }

        private async Task<IActionResult> ValidateAndRouteMessage(HttpRequest req, string messageTypeIdentifier, IBinder binder, ILogger log)
        {
            log.LogInformation("ShopifyWebhook recevied", messageTypeIdentifier);

            (bool requestIsValid, string requestBody) = await _validator.ValidateRequestAsync(req, shopifySharedSecret);
            if (!requestIsValid)
            {
                log.LogError("Invalid ShopifyWebhook recevied", messageTypeIdentifier);
                return new BadRequestResult();
            }

            var collector = await binder.BindAsync<IAsyncCollector<string>>(new ServiceBusAttribute(messageTypeIdentifier)
            {
                Connection = serviceBusConnectionSetting,
                EntityType = EntityType.Topic
            });
            
            await collector.AddAsync(requestBody);

            return new OkResult();
        }
    }
}

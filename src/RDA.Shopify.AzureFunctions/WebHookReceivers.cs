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

        private async Task<IActionResult> ValidateAndRouteMessage(HttpRequest req, string messageTypeIdentifier, IBinder binder, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            (bool requestIsValid, string requestBody) = await _validator.ValidateRequestAsync(req, shopifySharedSecret);
            if (!requestIsValid)
            {
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

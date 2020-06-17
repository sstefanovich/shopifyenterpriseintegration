using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using RDA.Shopify.Communication;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(RDA.Shopify.AzureFunctions.Startup))]
namespace RDA.Shopify.AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IWebHookValidator, WebHookValidator>();
        }
    }
}

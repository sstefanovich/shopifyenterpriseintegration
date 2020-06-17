using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RDA.Shopify.Communication
{
    public interface IWebHookValidator
    {
        Task<(bool, string)> ValidateRequestAsync(HttpRequest req, string sharedSecret);
    }
}

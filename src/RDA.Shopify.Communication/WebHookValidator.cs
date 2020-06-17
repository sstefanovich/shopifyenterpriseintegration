using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RDA.Shopify.Communication
{
    public class WebHookValidator : IWebHookValidator
    {
        public async Task<(bool, string)> ValidateRequestAsync(HttpRequest req, string sharedSecret)
        {
            StringValues headerValues;

            if (req.Headers.TryGetValue("X-Shopify-Hmac-Sha256", out headerValues))
            {
                string headerValue = headerValues.First();
                string requestBody;

                using (var sr = new StreamReader(req.Body))
                {
                    requestBody = await sr.ReadToEndAsync();
                }

                var keyBytes = Encoding.UTF8.GetBytes(sharedSecret);
                var dataBytes = Encoding.UTF8.GetBytes(requestBody);

                //use the SHA256Managed Class to compute the hash
                var hmac = new HMACSHA256(keyBytes);
                var hmacBytes = hmac.ComputeHash(dataBytes);

                //retun as base64 string. Compared with the signature passed in the header of the post request from Shopify. If they match, the call is verified.
                var createSignature = Convert.ToBase64String(hmacBytes);
                return (headerValue.Equals(createSignature), requestBody);

            }

            return (false, string.Empty);
        }
    }
}

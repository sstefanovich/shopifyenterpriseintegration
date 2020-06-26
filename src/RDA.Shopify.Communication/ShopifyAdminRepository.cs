using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RDA.Shopify.Communication
{
    public class ShopifyAdminRepository
    {
        private readonly GraphQLHttpClient _graphClient;

        private const string apiKey = "69a7c68590d2b75eaffefed03c84e800";
        private const string apiPassword = "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";
        private const string shopName = "steverdadev";

        public ShopifyAdminRepository()
        {
            _graphClient = new GraphQLHttpClient($"https://{shopName}.myshopify.com/admin/api/2020-04/graphql.json", new SystemTextJsonSerializer());
        }

        public async Task<TValue> ExecuteQuery<TValue>(string query)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{apiKey}:{apiPassword}");
            _graphClient.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var request = new GraphQLRequest()
            {
                Query = query
            };

            var response = await _graphClient.SendQueryAsync<TValue>(request);

            return response.Data;
        }

        public async Task<TValue> ExecuteMutation<TValue>(string query)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{apiKey}:{apiPassword}");
            _graphClient.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var request = new GraphQLRequest()
            {
                Query = query
            };

            var response = await _graphClient.SendMutationAsync<TValue>(request);

            return response.Data;
        }
    }
}

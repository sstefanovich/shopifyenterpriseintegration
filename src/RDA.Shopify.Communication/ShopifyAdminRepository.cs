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

        //Note, these credentials are not valid, substitute your own
        private const string apiKey = "XXXX";
        private const string apiPassword = "XXXX";
        private const string shopName = "steverdadev";

        public ShopifyAdminRepository()
        {
            _graphClient = new GraphQLHttpClient($"https://{shopName}.myshopify.com/admin/api/2020-04/graphql.json", new SystemTextJsonSerializer());
        }

        public async Task<TValue> ExecuteQuery<TValue>(string query, object variables = null)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{apiKey}:{apiPassword}");
            _graphClient.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var request = new GraphQLRequest()
            {
                Query = query
            };

            request.Variables = variables ?? null;

            var response = await _graphClient.SendQueryAsync<TValue>(request);

            return response.Data;
        }

        public async Task<TValue> ExecuteMutation<TValue>(string query, object variables = null)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{apiKey}:{apiPassword}");
            _graphClient.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var request = new GraphQLRequest()
            {
                Query = query
            };

            request.Variables = variables ?? null;

            var response = await _graphClient.SendMutationAsync<TValue>(request);

            return response.Data;
        }
    }
}

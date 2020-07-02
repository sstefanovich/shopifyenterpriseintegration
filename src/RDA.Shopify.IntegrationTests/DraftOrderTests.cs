using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDA.Shopify.Communication;
using RDA.Shopify.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RDA.Shopify.IntegrationTests
{
    [TestClass]
    public class DraftOrderTests
    {
        [TestMethod]
        public void Deserialize()
        {
            var json = @"{""draftOrderCreate"":{ ""draftOrder"":{ ""id"":""gid:\/\/shopify\/DraftOrder\/578904293450""},""userErrors"":[]}}";
            var deserialized = JsonSerializer.Deserialize<DraftOrderCreate>(json);

            Assert.IsNotNull(deserialized);

        }

        [TestMethod]
        public async Task DraftOrder_Create()
        {
            var shopifyRepo = new ShopifyAdminRepository();

            //First get the customer
            var query = @"query($queryParams: String!) {
                          customers(first:1, query:$queryParams) {
                            edges {
                              node {
                                id
                                verifiedEmail
                                firstName
                                lastName
                              }
                            }
                          }
                        }";

            var result = await shopifyRepo.ExecuteQuery<CustomersList>(query, 
                new 
                { 
                    queryParams = "email:steve@digitalfringe.com"
                });

            Assert.IsNotNull(result);

            //Now create a draft order
            var customerId = result.Customers.edges[0].node.Id;

            var mutation = @"mutation draftOrderCreate($input: DraftOrderInput!) {
                                draftOrderCreate(input: $input) {
                                    draftOrder {
                                        id
                                    }
                                    userErrors {
                                        field
                                        message
                                    }
                                }
                            }";

            var draftOrderInput = new
            {
                customerId = customerId,
                lineItems = new[] {
                   new { title = "Test Item 1", quantity = 1, originalUnitPrice = 159.99, requiresShipping = false, sku = "TestSku123", taxable = false }
            }
            };

            var orderResult = await shopifyRepo.ExecuteQuery<DraftOrderCreateResponse>(mutation,
                new
                {
                    input = draftOrderInput
                });

            //Finally send order to customer for payment
            var orderId = orderResult.DraftOrderCreate.DraftOrder.Id;

            var orderSendMutation = @"mutation draftOrderInvoiceSend($id: ID!) {
                                        draftOrderInvoiceSend(id: $id) {
                                            draftOrder {
                                                id
                                            }
                                            userErrors {
                                                field
                                                message
                                            }
                                        }
                                    }";


            var orderSendResult = await shopifyRepo.ExecuteQuery<DraftOrderInvoiceSendResponse>(orderSendMutation,
                new
                {
                    id = orderId
                });

            Assert.IsTrue(orderSendResult.DraftOrderInvoiceSend.DraftOrder.Id == orderId);
        }
    }
}

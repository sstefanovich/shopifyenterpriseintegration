using System;

namespace RDA.Shopify.Entities
{
    public class CustomersList
    {
        public CollectionResult<Customer> Customers { get; set; }
    }

    public class Customer
    {
        public string Id { get; set; }

        public bool VerifiedEmail { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}

namespace EFCore6CosmosValueConverter
{
    internal class Customer
    {
        public CustomerId id { get; set; }
        public string partitionKey { get; set; }
        public string Name { get; set; }

        public Customer()
        {

        }
    }

    class CustomerId : TypedId<Guid>
    {
        public CustomerId(Guid value) : base(value)
        {
        }

        public static CustomerId New()
        {
            return new CustomerId(Guid.NewGuid());
        }
    }
}

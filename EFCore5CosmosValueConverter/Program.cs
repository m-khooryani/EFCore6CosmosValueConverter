using EFCore6CosmosValueConverter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCore5CosmosValueConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // config
            var cosmosDbAccountEndpoint = "https://localhost:8081/";
            var cosmosDbAccountKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
            var databaseName = "test5.0";

            // dbContext
            var dbOptionsBuilder = new DbContextOptionsBuilder<TestContext>();
            // replace IValueConverterSelector service
            dbOptionsBuilder.ReplaceService<IValueConverterSelector, TypedIdValueConverterSelector>();
            dbOptionsBuilder.UseCosmos(cosmosDbAccountEndpoint, cosmosDbAccountKey, databaseName);

            // act
            using TestContext context = new(dbOptionsBuilder.Options);
            context.Database.EnsureCreated();
            var id = CustomerId.New();
            var customer = new Customer()
            {
                id = id,
                Name = "customer1",
                partitionKey = id.ToString(),
            };
            context.Customers.Add(customer);
            context.SaveChanges();
        }
    }
}

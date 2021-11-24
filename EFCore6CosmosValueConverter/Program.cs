using EFCore6CosmosValueConverter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

// config
var cosmosDbAccountEndpoint = "https://localhost:8081/";
var cosmosDbAccountKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
var databaseName = "test6.0";

// dbContext
var dbOptionsBuilder = new DbContextOptionsBuilder<TestContext>();
dbOptionsBuilder.UseCosmos(cosmosDbAccountEndpoint, cosmosDbAccountKey, databaseName);

// replace IValueConverterSelector service
dbOptionsBuilder.ReplaceService<IValueConverterSelector, TypedIdValueConverterSelector>();

using (TestContext context = new(dbOptionsBuilder.Options))
{
    context.Database.EnsureCreated();
}

using EFCore6CosmosValueConverter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasPartitionKey(x => x.partitionKey);

        builder.Property(x => x.id).ToJsonProperty("id");
        builder.Property(x => x.partitionKey).ToJsonProperty("partitionKey");
        builder.Property(x => x.Name).ToJsonProperty("name");

        builder.HasKey(x => x.id);

        builder.HasDiscriminator();

        builder.ToContainer("customers");
    }
}
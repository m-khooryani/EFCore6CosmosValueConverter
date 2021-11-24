using Microsoft.EntityFrameworkCore;

namespace EFCore6CosmosValueConverter
{
    class TestContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

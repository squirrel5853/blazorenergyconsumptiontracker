using energyconsumptiontracker.Domain;
using Microsoft.EntityFrameworkCore;

namespace energyconsumptiontracker.Persistence
{
    public class MeterReadingDbContext : DbContext
    {
        public DbSet<MeterReading> MeterReadings { get; set; }
        public DbSet<CustomerAccount> Customers { get; set; }

        public MeterReadingDbContext(DbContextOptions<MeterReadingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureCustomerAccountEntityType(modelBuilder);

            ConfigureMeterReadingEntityType(modelBuilder);
        }

        private void ConfigureMeterReadingEntityType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeterReading>().HasKey(x => x.Id);
            modelBuilder.Entity<MeterReading>().HasIndex(x => x.AccountId);
            modelBuilder.Entity<MeterReading>().HasIndex(x => new { x.MeterReadingDate, x.AccountId }).IsUnique();
        }

        private void ConfigureCustomerAccountEntityType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerAccount>().HasKey(x => x.AccountId);
        }
    }

}

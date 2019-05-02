using CheckPromise.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckPromise.Data.DataContext
{
    public class CheckPromiseContext : DbContext
    {
        public CheckPromiseContext(DbContextOptions<CheckPromiseContext> options)
            : base(options)
        { }

        public DbSet<Promise> Promises { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<IndicatorValue> IndicatorValues { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Promise>()
                .Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Promise>()
                .Property(p => p.Value).IsRequired();
            modelBuilder.Entity<Promise>()
                .Property(p => p.IsCompleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<Indicator>()
                .Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<IndicatorValue>()
                .Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<IndicatorValue>()
                .Property(p => p.Date)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<IndicatorValue>()
                .HasOne(iv =>iv.Indicator)
                .WithMany(i => i.Values)
                .HasForeignKey(iv => iv.Indicator)
                .HasConstraintName("ForeignKey_IndicatorValue_Indicator");

            modelBuilder.Entity<Currency>()
                .Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<ExchangeRate>()
                .Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ExchangeRate>()
                .Property(p => p.Date)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<ExchangeRate>()
                .HasOne(er => er.Currency)
                .WithMany(c => c.Rates)
                .HasForeignKey(er => er.Currency)
                .HasConstraintName("ForeignKey_ExchangeRate_Currency");
        }
    }
}

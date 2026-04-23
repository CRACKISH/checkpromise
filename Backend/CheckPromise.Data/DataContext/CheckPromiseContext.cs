using CheckPromise.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckPromise.Data.DataContext;

public class CheckPromiseContext(DbContextOptions<CheckPromiseContext> options) : DbContext(options)
{
    public DbSet<Promise> Promises => Set<Promise>();

    public DbSet<Indicator> Indicators => Set<Indicator>();

    public DbSet<IndicatorValue> IndicatorValues => Set<IndicatorValue>();

    public DbSet<GraphData> GraphData => Set<GraphData>();

    public DbSet<MediaInfo> MediaInfo => Set<MediaInfo>();

    public DbSet<News> News => Set<News>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Promise>(entity =>
        {
            entity.ToTable("Promise");
            entity.Property(p => p.Status).HasDefaultValue(PromiseStatus.Nothing);
        });

        modelBuilder.Entity<Indicator>(entity =>
        {
            entity.ToTable("Indicator");
            entity.Property(i => i.InvertArrow).HasDefaultValue(false);
            entity.HasMany(i => i.Values)
                .WithOne(v => v.Indicator!)
                .HasForeignKey(v => v.IndicatorId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(i => i.GraphData)
                .WithOne(g => g.Indicator!)
                .HasForeignKey(g => g.IndicatorId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(i => i.MediaInfoData)
                .WithOne(m => m.Indicator!)
                .HasForeignKey(m => m.IndicatorId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<IndicatorValue>(entity =>
        {
            entity.ToTable("IndicatorValue");
            entity.Property(v => v.Date).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<GraphData>(entity =>
        {
            entity.ToTable("GraphData");
            entity.Property(g => g.Date).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<MediaInfo>(entity =>
        {
            entity.ToTable("MediaInfo");
            entity.Property(m => m.Date).HasDefaultValueSql("GETDATE()");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.ToTable("News");
            entity.Property(n => n.Date).HasDefaultValueSql("GETDATE()");
        });
    }
}

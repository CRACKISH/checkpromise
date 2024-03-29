﻿using CheckPromise.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckPromise.Data.DataContext
{
    public class CheckPromiseContext : DbContext
    {
        public CheckPromiseContext(DbContextOptions<CheckPromiseContext> options) : base(options) { }

        public DbSet<Promise> Promise { get; set; }
		public DbSet<Indicator> Indicator { get; set; }
        public DbSet<IndicatorValue> IndicatorValue { get; set; }
		public DbSet<GraphData> GraphData { get; set; }
		public DbSet<MediaInfo> MediaInfo { get; set; }
		//public DbSet<Currency> Currency { get; set; }
        //public DbSet<ExchangeRate> ExchangeRate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Promise>()
                .Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Promise>()
                .Property(p => p.Value).IsRequired();
            modelBuilder.Entity<Promise>()
                .Property(p => p.Status)
                .HasDefaultValue(PromiseStatus.Nothing);
			modelBuilder.Entity<Promise>().ToTable("Promise");

			modelBuilder.Entity<Indicator>()
                .Property(i => i.Id).ValueGeneratedOnAdd();
			modelBuilder.Entity<Indicator>()
				.Property(i => i.InvertArrow)
				.IsRequired()
				.HasDefaultValue(false);
			modelBuilder.Entity<Indicator>().ToTable("Indicator");

			modelBuilder.Entity<IndicatorValue>()
                .Property(iv => iv.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<IndicatorValue>()
                .Property(iv => iv.Date)
                .HasDefaultValueSql("getdate()");
			modelBuilder.Entity<IndicatorValue>()
				.Property(iv => iv.Measure)
				.HasDefaultValue(Measure.Empty);
			/*modelBuilder.Entity<IndicatorValue>()
                .HasOne(iv =>iv.Indicator)
                .WithMany(i => i.Value)
                .HasForeignKey(iv => iv.Indicator)
                .HasConstraintName("ForeignKey_IndicatorValue_Indicator");*/
			modelBuilder.Entity<IndicatorValue>().ToTable("IndicatorValue");

			modelBuilder.Entity<GraphData>()
				.Property(p => p.Id).ValueGeneratedOnAdd();
			modelBuilder.Entity<GraphData>()
				.Property(gd => gd.Date)
				.IsRequired()
				.HasDefaultValueSql("getdate()");
			/*modelBuilder.Entity<GraphData>()
				.HasOne(gd => gd.Indicator)
				.WithMany(i => i.GraphData)
				.HasForeignKey(gd => gd.Indicator)
				.HasConstraintName("ForeignKey_GraphData_Indicator");*/
			modelBuilder.Entity<GraphData>().ToTable("GraphData");

			modelBuilder.Entity<MediaInfo>()
				.Property(mi => mi.Id).ValueGeneratedOnAdd();
			modelBuilder.Entity<MediaInfo>()
				.Property(mi => mi.Date)
				.IsRequired()
				.HasDefaultValueSql("getdate()");
			modelBuilder.Entity<MediaInfo>()
				.Property(mi => mi.Caption).IsRequired();
			modelBuilder.Entity<MediaInfo>()
				.Property(mi => mi.Source).IsRequired();
			/*modelBuilder.Entity<MediaInfo>()
				.HasOne(mi => mi.Indicator)
				.WithMany(i => i.MediaInfoData);
				//.HasForeignKey(mi => mi.Indicator)
				//.HasConstraintName("ForeignKey_MediaInfo_Indicator");*/
			modelBuilder.Entity<MediaInfo>().ToTable("MediaInfo");

			/*modelBuilder.Entity<Currency>()
                .Property(p => p.Id).ValueGeneratedOnAdd();
			modelBuilder.Entity<Currency>().ToTable("Currency");

			modelBuilder.Entity<ExchangeRate>()
                .Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ExchangeRate>()
                .Property(p => p.Date)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<ExchangeRate>()
                .HasOne(er => er.Currency)
                .WithMany(c => c.Rates)
                .HasForeignKey(er => er.Currency)
                .HasConstraintName("ForeignKey_ExchangeRate_Currency");*/
		}
	}
}

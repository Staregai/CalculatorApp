using Microsoft.EntityFrameworkCore;
using CalculatorApp.Models;
using System.IO;
using System;

namespace CalculatorApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<CalculationHistory> CalculationHistories => Set<CalculationHistory>();
        public DbSet<CurrencyRate> CurrencyRates => Set<CurrencyRate>();
        public DbSet<CurrencyConversionHistory> CurrencyConversionHistories => Set<CurrencyConversionHistory>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var dbPath = Path.Combine(AppContext.BaseDirectory, "AppData", "calculator.db");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyRate>().HasKey(x => new { x.Code, x.EffectiveDate });
            base.OnModelCreating(modelBuilder);
        }

        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
    }
}

using CalculatorApp.Data;
using CalculatorApp.Models;
using CalculatorApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Calculator_tests
{
    [TestClass]
    public class IntegrationTests
    {
        private DbContextOptions<AppDbContext> _dbOptions;

        [TestInitialize]
        public void Setup()
        {
            _dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [TestMethod]
        public void CalculationHistory_CanAddAndRead()
        {
            using var db = new AppDbContext(_dbOptions);
            db.CalculationHistories.Add(new CalculationHistory
            {
                Expression = "2+2",
                Result = "4",
                TimestampUtc = DateTime.UtcNow
            });
            db.SaveChanges();

            var item = db.CalculationHistories.FirstOrDefault(x => x.Expression == "2+2");
            Assert.IsNotNull(item);
            Assert.AreEqual("4", item.Result);
        }

        [TestMethod]
        public void CurrencyRate_CanAddAndQuery()
        {
            using var db = new AppDbContext(_dbOptions);
            db.CurrencyRates.Add(new CurrencyRate
            {
                Code = "USD",
                EffectiveDate = DateTime.Today,
                Mid = 4.5m
            });
            db.SaveChanges();

            var rate = db.CurrencyRates.FirstOrDefault(x => x.Code == "USD");
            Assert.IsNotNull(rate);
            Assert.AreEqual(4.5m, rate.Mid);
        }

        [TestMethod]
        public void CurrencyConversionHistory_CanAddAndQuery()
        {
            using var db = new AppDbContext(_dbOptions);
            db.CurrencyConversionHistories.Add(new CurrencyConversionHistory
            {
                Operation = "PLN->USD",
                Currency = "USD",
                Rate = 4.5m,
                AmountInput = 100,
                AmountOutput = 22.22m,
                TimestampUtc = DateTime.UtcNow
            });
            db.SaveChanges();

            var conv = db.CurrencyConversionHistories.FirstOrDefault(x => x.Operation == "PLN->USD");
            Assert.IsNotNull(conv);
            Assert.AreEqual("USD", conv.Currency);
        }

        [TestMethod]
        public async Task CurrencyService_BestDayAsync_ReturnsBest()
        {
            using var db = new AppDbContext(_dbOptions);
            db.CurrencyRates.Add(new CurrencyRate { Code = "USD", EffectiveDate = DateTime.Today.AddDays(-1), Mid = 4.0m });
            db.CurrencyRates.Add(new CurrencyRate { Code = "USD", EffectiveDate = DateTime.Today, Mid = 4.5m });
            db.SaveChanges();

            var service = new CurrencyService(db);
            var best = await service.BestDayAsync("USD", DateTime.Today.AddDays(-2), DateTime.Today);
            Assert.IsNotNull(best);
            Assert.AreEqual(4.5m, best.Value.rate);
        }
    }
}
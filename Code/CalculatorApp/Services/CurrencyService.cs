using CalculatorApp.Data;
using CalculatorApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CalculatorApp.Services
{
    public class CurrencyService
    {
        private readonly AppDbContext _db;
        private static readonly HttpClient _http = new HttpClient();

        private record NbpResponse(string code, Rate[] rates);
        private record Rate(string no, DateTime effectiveDate, decimal mid);

        public CurrencyService(AppDbContext db) => _db = db;

        public async Task<int> FetchAndStoreRatesAsync(string code, DateTime from, DateTime to)
        {
            string url = $"https://api.nbp.pl/api/exchangerates/rates/A/{code}/{from:yyyy-MM-dd}/{to:yyyy-MM-dd}?format=json";
            var json = await _http.GetStringAsync(url);
            var nbp = JsonSerializer.Deserialize<NbpResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (nbp?.rates == null) return 0;

            foreach (var r in nbp.rates)
            {
                var kCode = nbp.code.ToUpperInvariant();
                var kDate = r.effectiveDate.Date;
                var existing = await _db.CurrencyRates.FindAsync(kCode, kDate);
                if (existing == null)
                {
                    _db.CurrencyRates.Add(new CurrencyRate { Code = kCode, EffectiveDate = kDate, Mid = r.mid });
                }
                else
                {
                    existing.Mid = r.mid;
                    _db.CurrencyRates.Update(existing);
                }
            }
            return await _db.SaveChangesAsync();
        }

        public async Task<(DateTime day, decimal rate)?> BestDayAsync(string code, DateTime from, DateTime to)
        {
            var list = await _db.CurrencyRates
                .Where(x => x.Code == code.ToUpper() && x.EffectiveDate >= from.Date && x.EffectiveDate <= to.Date)
                .ToListAsync();

            var r = list.OrderByDescending(x => x.Mid).FirstOrDefault();
            if (r == null) return null;
            return (r.EffectiveDate, r.Mid);
        }
    }
}

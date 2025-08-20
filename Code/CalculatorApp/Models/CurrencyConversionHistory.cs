using System;
using System.ComponentModel.DataAnnotations;

namespace CalculatorApp.Models
{
    public class CurrencyConversionHistory
    {
        [Key] public int Id { get; set; }
        public string Operation { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public decimal AmountInput { get; set; }
        public decimal AmountOutput { get; set; }
        public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
    }
}

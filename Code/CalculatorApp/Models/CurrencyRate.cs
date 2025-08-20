using System;
using System.ComponentModel.DataAnnotations;

namespace CalculatorApp.Models
{
    public class CurrencyRate
    {
        [Key] public string Code { get; set; } = "USD";
        [Key] public DateTime EffectiveDate { get; set; }
        public decimal Mid { get; set; }
    }
}

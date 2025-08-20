using System;
using System.ComponentModel.DataAnnotations;

namespace CalculatorApp.Models
{
    public class CalculationHistory
    {
        [Key] public int Id { get; set; }
        public string Expression { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
    }
}

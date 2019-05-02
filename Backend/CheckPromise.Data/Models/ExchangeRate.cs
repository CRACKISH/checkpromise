using System;

namespace CheckPromise.Data.Models
{
    public class ExchangeRate
    {
        public Guid Id { get; set; }
        public Currency Currency { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }
}

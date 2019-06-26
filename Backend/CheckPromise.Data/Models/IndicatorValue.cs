using System;

namespace CheckPromise.Data.Models
{
    public class IndicatorValue
    {
        public int Id { get; set; }
        public Indicator Indicator { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }
}

using System;
using System.ComponentModel;

namespace CheckPromise.Data.Models
{
	public enum Measure {
		[Description("")]
		Empty,
		[Description("₴")]
		UAH,
		[Description("$")]
		USD,
		[Description("%")]
		Parcent
	}

	public class IndicatorValue
    {
        public int Id { get; set; }
        public Indicator Indicator { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }

		public Measure Measure { get; set; }

		public string Quantity { get; set; }
	}
}

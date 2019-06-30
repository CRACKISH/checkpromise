using System;

namespace CheckPromise.Data.Models
{
	public class GraphData
	{
		public int Id { get; set; }
		public Indicator Indicator { get; set; }
		public DateTime Date { get; set; }
		public string Value { get; set; }
	}
}

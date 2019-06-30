using System;

namespace CheckPromise.Data.Models
{
	public class MediaInfo
	{
		public int Id { get; set; }
		public Indicator Indicator { get; set; }
		public DateTime Date { get; set; }
		public string Caption { get; set; }
		public string Source { get; set; }
	}
}

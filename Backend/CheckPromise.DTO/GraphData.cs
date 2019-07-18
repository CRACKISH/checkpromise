using System;
using System.Runtime.Serialization;

namespace CheckPromise.DTO
{
	[DataContract]
	public class GraphData
	{
		[DataMember(Name = "date")]
		public DateTime Date { get; set; }
		[DataMember(Name = "value")]
		public string Value { get; set; }

		public GraphData(Data.Models.GraphData graphData) {
			Date = graphData.Date;
			Value = graphData.Value;
		}
	}
}

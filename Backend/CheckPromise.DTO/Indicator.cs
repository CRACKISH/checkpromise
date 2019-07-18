using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CheckPromise.DTO
{
	[DataContract]
	public class Indicator
    {
		[DataMember(Name = "id")]
		public int Id { get; set; }
		[DataMember(Name = "label")]
		public string Label { get; set; }
		[DataMember(Name = "invertArrow")]
		public bool InvertArrow { get; set; }
		[DataMember(Name = "graphData")]
		public IEnumerable<GraphData> GraphData { get; set; }
		[DataMember(Name = "mediaInfoData")]
		public IEnumerable<MediaInfo> MediaInfoData { get; set; }

		public Indicator(Data.Models.Indicator indicator) {
			Id = indicator.Id;
			Label = indicator.Label;
			InvertArrow = indicator.InvertArrow;
			GraphData = indicator.GraphData?.Select(gd => new GraphData(gd)).ToList();
			MediaInfoData = indicator.MediaInfoData?.Select(mi => new MediaInfo(mi)).ToList();
		}
	}
}

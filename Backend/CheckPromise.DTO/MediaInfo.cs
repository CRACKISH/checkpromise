using System;
using System.Runtime.Serialization;

namespace CheckPromise.DTO
{
	[DataContract]
	public class MediaInfo
	{
		[DataMember(Name = "date")]
		public DateTime Date { get; set; }
		[DataMember(Name = "caption")]
		public string Caption { get; set; }
		[DataMember(Name = "source")]
		public string Source { get; set; }

		public MediaInfo(Data.Models.MediaInfo mediaInfo) {
			Date = mediaInfo.Date;
			Caption = mediaInfo.Caption;
			Source = mediaInfo.Source;
		}
	}
}

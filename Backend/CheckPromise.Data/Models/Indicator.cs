using System.Collections.Generic;

namespace CheckPromise.Data.Models
{
    public class Indicator
    {
        public int Id { get; set; }
        public string Label { get; set; }
		public bool InvertArrow { get; set; }
		public string Source { get; set; }
		//public IEnumerable<IndicatorValue> Values { get; set; }
		public IEnumerable<GraphData> GraphData { get; set; }
		public virtual IEnumerable<MediaInfo> MediaInfoData { get; set; }
	}
}

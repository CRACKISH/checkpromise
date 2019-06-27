using CheckPromise.Data.Models;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CheckPromise.DTO
{
	[DataContract()]
	public class ClientData
    {
		[DataMember(Name = "indicatorData")]
		public IEnumerable<Indicator> IndicatorData { get; set; }
		[DataMember(Name = "promiseData")]
		public IEnumerable<Promise> PromiseData { get; set; }
	}
}

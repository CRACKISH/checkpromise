using System.Runtime.Serialization;

namespace CheckPromise.DTO
{
	[DataContract]
	public class Promise
    {
		[DataMember(Name = "value")]
		public string Value { get; set; }
		[DataMember(Name = "isCompleted")]
		public bool IsCompleted { get; set; }
		[DataMember(Name = "source")]
		public string Source { get; set; }

		public Promise(Data.Models.Promise promise) {
			Value = promise.Value;
			IsCompleted = promise.IsCompleted;
			Source = promise.Source;
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace CheckPromise.Data.Models
{
    public class Promise
    {
		[DataMember(Name = "id")]
		public int Id { get; set; }
		[DataMember(Name = "value")]
		public string Value { get; set; }
		[DataMember(Name = "isCompleted")]
		public bool IsCompleted { get; set; }
		[DataMember(Name = "source")]
		public string Source { get; set; }
    }
}

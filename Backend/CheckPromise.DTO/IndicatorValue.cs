using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace CheckPromise.DTO
{
	[DataContract]
	public class IndicatorValue
	{
		[DataMember(Name = "date")]
		public DateTime Date { get; set; }
		[DataMember(Name = "value")]
		public double Value { get; set; }
		[DataMember(Name = "value2", EmitDefaultValue = false)]
		public double Value2 { get; set; }
		[DataMember(Name = "measure")]
		public string Measure { get; set; }
		[DataMember(Name = "quantity", EmitDefaultValue=false)]
		public string Quantity { get; set; }

		protected string ToDescriptionString(Data.Models.Measure measure)
		{
			DescriptionAttribute[] attributes = (DescriptionAttribute[])measure
			   .GetType()
			   .GetField(measure.ToString())
			   .GetCustomAttributes(typeof(DescriptionAttribute), false);
			return attributes.Length > 0 ? attributes[0].Description : string.Empty;
		}

		public IndicatorValue(Data.Models.IndicatorValue indicatorValue)
		{
			Date = indicatorValue.Date;
			Value = indicatorValue.Value;
			Value2 = indicatorValue.Value;
			Measure = ToDescriptionString(indicatorValue.Measure);
			Quantity = indicatorValue.Quantity;
		}
	}
}

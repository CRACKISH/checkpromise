using System;
using System.Collections.Generic;

namespace CheckPromise.Data.Models
{
    public class Indicator
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public IEnumerable<IndicatorValue> Values { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CheckPromise.Data.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public IEnumerable<ExchangeRate> Rates { get; set; }
    }
}

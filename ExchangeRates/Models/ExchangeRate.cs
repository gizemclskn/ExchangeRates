using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeRates.Models
{
    public class ExchangeRate
    {
        public string Currency { get; set; }
        public string BuyingRate { get; set; }
        public DateTime Date { get; set; }
    }
}

using System;

namespace currencyClassLibrary
{
    public class CurrencyResults
    {
        public DateTime TradingDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public ExchangeRate[] Rates { get; set; }
    }

    public class ExchangeRate
    {
        public string Currency { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
    }

}

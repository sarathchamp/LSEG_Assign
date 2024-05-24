namespace StockExchangeAPI.Models
{
    public class Outlier
    {
        public string Ticker { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal StockPrice { get; set; }
        public decimal Mean { get; set; }
        public decimal Deviation { get; set; }
        public decimal PercentDeviation { get; set; }
    }
}

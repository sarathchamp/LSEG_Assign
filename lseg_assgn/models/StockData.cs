namespace StockExchangeAPI.Models
{
    public class StockData
    {
        public string Ticker { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal StockPrice { get; set; }
    }
}

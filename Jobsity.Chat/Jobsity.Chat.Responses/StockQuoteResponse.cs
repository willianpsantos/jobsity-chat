using System.Globalization;

namespace Jobsity.Chat.Responses
{
    public class StockQuoteResponse
    {
        public string? Symbol { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public double? Open { get; set; }
        public double? High { get; set; }
        public double? Low { get; set; }
        public double? Close { get; set; }
        public double? Volume { get; set; }

        public override string ToString() => 
            $"{Symbol} quote is {Close?.ToString("C", new CultureInfo("en-US"))} per share";
    }
}

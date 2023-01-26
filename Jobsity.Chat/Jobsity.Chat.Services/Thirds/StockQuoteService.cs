using Jobsity.Chat.Interfaces.Identity;
using Jobsity.Chat.Interfaces.Services.Thirds;
using Jobsity.Chat.Options;
using Jobsity.Chat.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Xml;

namespace Jobsity.Chat.Services.Thirds
{
    public class StockQuoteService : BaseService, IStockQuoteService
    {
        private readonly StockQuoteOptions _stockOptions;

        public StockQuoteService(
            IIdentityService identityService, 
            IHttpContextAccessor contextAcessor,
            IOptions<StockQuoteOptions> stockOptions
        ) : base(identityService, contextAcessor)
        {
            _stockOptions = stockOptions.Value;
        }

        private async Task<IEnumerable<StockQuoteResponse>> GetStockCSV(string code)
        {
            var url = string.Format(_stockOptions.Url, code);
            var stocks = new HashSet<StockQuoteResponse>();

            using(var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var lineCount = 0;
                var line = "";

                using (var csvReader = new StringReader(response))
                {
                    do
                    {
                        line = await csvReader.ReadLineAsync();

                        if(string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        if (lineCount == 0)
                        {
                            lineCount++;
                            continue;
                        }

                        var split = line.Split(",");

                        var stock = new StockQuoteResponse();
                        var culture = new CultureInfo("en-US");

                        stock.Symbol = split[0];
                        stock.Date = DateTime.Parse(string.Format("{0} {1}", split[1], split[2]));
                        stock.Time = split[2];
                        stock.Open = double.Parse(split[3], culture);
                        stock.High = double.Parse(split[4], culture);
                        stock.Low = double.Parse(split[5], culture);
                        stock.Close = double.Parse(split[6], culture);
                        stock.Volume = double.Parse(split[7], culture);

                        stocks.Add(stock);
                        lineCount++;
                    }
                    while (!string.IsNullOrEmpty(line) && !string.IsNullOrWhiteSpace(line));
                }
            }

            return stocks;
        }

        public bool IsAStockQuoteCommand(string text) => Regex.IsMatch(text, _stockOptions.CommandCheckPattern);

        public string GetStockCode(string text)
        {
            var code = text.Replace(_stockOptions.Command, "");
            return code;
        }

        public async Task<IUser?> GetStockUserAsync() => await _identityService.GetUserById(_stockOptions.StockUserId);

        public async Task<StockQuoteResponse?> GetStockQuoteAsync(string code)
        {
            var stocks = await GetStockCSV(code);
            var stock = stocks?.FirstOrDefault();

            return stock;
        }
    }
}

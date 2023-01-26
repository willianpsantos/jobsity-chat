using Jobsity.Chat.Interfaces.Identity;
using Jobsity.Chat.Responses;

namespace Jobsity.Chat.Interfaces.Services.Thirds
{
    public interface IStockQuoteService
    {
        bool IsAStockQuoteCommand(string text);
        string GetStockCode(string text);

        Task<IUser?> GetStockUserAsync();
        Task<StockQuoteResponse?> GetStockQuoteAsync(string code);
    }
}

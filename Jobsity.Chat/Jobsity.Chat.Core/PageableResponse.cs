using Jobsity.Chat.Core.Interfaces;

namespace Jobsity.Chat.Core
{
    public class PageableResponse<TData> : IPageableResponse<TData>
    {
        public int Page { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<TData> Data { get; set; }
    }
}
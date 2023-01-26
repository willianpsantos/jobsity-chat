namespace Jobsity.Chat.Core.Interfaces
{
    public interface IPageableResponse<TData>
    {
        int Page { get; set; }
        int PageSize { get; set; }
        int Count { get; set; }
        IEnumerable<TData> Data { get; set; }
    }
}

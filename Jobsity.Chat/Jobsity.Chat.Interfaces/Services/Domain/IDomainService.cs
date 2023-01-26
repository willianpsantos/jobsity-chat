using Jobsity.Chat.Core.Interfaces;

namespace Jobsity.Chat.Interfaces.Services.Domain
{
    public interface IDomainService<
        TSaveRequest, 
        TSaveResponse, 
        TFindByIdResponse, 
        TGetAllResponse, 
        TGetPaginatedResponse,
        TDeleteResponse>
    {
        Task<TSaveResponse> SaveAsync(TSaveRequest request);
        Task<TFindByIdResponse> FindByIdAsync(Guid id);
        Task<IEnumerable<TGetAllResponse>> GetAsync();
        Task<IPageableResponse<TGetPaginatedResponse>> GetPaginatedAsync(int page, int pageSize);        
        Task<TDeleteResponse> DeleteAsync(Guid id);
    }
}

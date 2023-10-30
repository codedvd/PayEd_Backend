using PayEd.Data.Dto;
using PayEd.Infrastructure.Helpers;

namespace PayEd.Core.Services
{
    public interface IStreamRepository
    {
        Task<ApiResponse> CreateStreamAsync(Guid userId, StreamDto income);
        Task<ApiResponse> DeleteStreamAsync(Guid streamID);
        Task<ApiResponse> GetStreamAsync(Guid streamID);
        Task<ApiResponse> GetStreamsAsync();
        Task<ApiResponse> UpdateStreamAsync(Guid streamID, StreamDto incomeStreamDto);
    }
}

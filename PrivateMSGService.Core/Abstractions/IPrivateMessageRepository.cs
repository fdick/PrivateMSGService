using PrivateMSGService.Core.Models;

namespace PrivateMSGService.Core.Abstractions
{
    public interface IPrivateMessageRepository
    {
        Task<Guid> CreateAsync(PrivateMessage msg);
        Task<Guid> DeleteAsync(Guid id);
        Task<List<(PrivateMessage, string)>> GetAllAsync();
        Task<(PrivateMessage, string)> GetOneAsync(Guid id);
        Task<Guid> UpdateAsync(Guid Id, Guid toUserId, Guid fromUserId, string message, DateTime sentTime);
    }
}
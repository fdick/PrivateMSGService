using PrivateMSGService.Core.Models;

namespace PrivateMSGService.Core.Abstractions
{
    public interface IPrivateMessagesService
    {
        Task<Guid> CreatePrivateMsgAsync(PrivateMessage message);
        Task<Guid> DeletePrivateMsgAsync(Guid Id);
        Task<List<(PrivateMessage, string)>> GetAllAsync();
        Task<(PrivateMessage, string)> GetOneAsync(Guid Id);
        Task<Guid> UpdatePrivateMsgAsync(Guid Id, Guid toUserId, Guid fromUserId, string message, DateTime sentTime);
    }
}
using PrivateMSGService.Core.Abstractions;
using PrivateMSGService.Core.Models;

namespace PrivateMSGService.Application.Services
{
    public class PrivateMessagesService : IPrivateMessagesService
    {
        private readonly IPrivateMessageRepository _repository;

        public PrivateMessagesService(IPrivateMessageRepository repository)
        {
            this._repository = repository;
        }

        public async Task<List<(PrivateMessage, string)>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<(PrivateMessage, string)> GetOneAsync(Guid Id)
        {
            return await _repository.GetOneAsync(Id);
        }

        public async Task<Guid> CreatePrivateMsgAsync(PrivateMessage message)
        {
            return await _repository.CreateAsync(message);
        }

        public async Task<Guid> DeletePrivateMsgAsync(Guid Id)
        {
            return await _repository.DeleteAsync(Id);
        }

        public async Task<Guid> UpdatePrivateMsgAsync(Guid Id, Guid toUserId, Guid fromUserId, string message, DateTime sentTime)
        {
            return await _repository.UpdateAsync(Id, toUserId, fromUserId, message, sentTime);
        }
    }
}

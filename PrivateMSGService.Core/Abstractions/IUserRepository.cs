using PrivateMSGService.Core.Models;

namespace PrivateMSGService.Core.Abstractions
{
    public interface IUserRepository
    {
        Task<Guid> Create(User user);
        Task<Guid> Delete(Guid id);
        Task<List<(User, string)>> GetAll();
        Task<(User, string)> GetOneAsync(Guid id);
        Task<Guid> Update(Guid id, string name, string lastname, string nickname, string email);
    }
}
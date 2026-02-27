using PrivateMSGService.Core.Models;

namespace PrivateMSGService.Core.Abstractions
{
    public interface IUsersService
    {
        Task<Guid> CreateUserAsync(User user);
        Task<Guid> DeleteUserAsync(Guid id);
        Task<List<(User, string)>> GetAllUsersAsync();
        Task<Guid> UpdateUserAsync(Guid id, string name, string lastname, string email, string nickname);
    }
}
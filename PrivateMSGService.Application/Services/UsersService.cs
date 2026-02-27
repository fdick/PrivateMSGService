using PrivateMSGService.Core.Abstractions;
using PrivateMSGService.Core.Models;

namespace PrivateMSGService.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _repository;

        public UsersService(IUserRepository repository)
        {
            this._repository = repository;
        }

        public async Task<List<(User, string)>> GetAllUsersAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<Guid> CreateUserAsync(User user)
        {
            return await _repository.Create(user);
        }

        public async Task<Guid> DeleteUserAsync(Guid id)
        {
            return await _repository.Delete(id);
        }

        public async Task<Guid> UpdateUserAsync(Guid id, string name, string lastname, string email, string nickname)
        {
            return await _repository.Update(id, name, lastname, nickname, email);
        }

    }
}

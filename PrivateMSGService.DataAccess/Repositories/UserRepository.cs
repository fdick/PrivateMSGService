using Microsoft.EntityFrameworkCore;
using PrivateMSGService.Core.Abstractions;
using PrivateMSGService.Core.Models;
using PrivateMSGService.DataAccess.Entities;
using System.Linq.Expressions;

namespace PrivateMSGService.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PrivateMSGDbContext _context;

        public UserRepository(PrivateMSGDbContext context)
        {
            this._context = context;
        }

        public async Task<List<(User, string)>> GetAll()
        {
            var entities = await FindAsync();

            return entities.Select(x => User.Create(x.ID, x.Nickname, x.Name, x.LastName, x.Email)).ToList();
        }

        public async Task<(User, string)> GetOneAsync(Guid id)
        {
            var entities = await FindAsync(predicate: x => x.ID == id);
            var entity = entities.FirstOrDefault();

            var error = string.Empty;
            (User, string) user;

            if (entity != null)
            {
                user = User.Create(entity.ID, entity.Nickname, entity.Name, entity.LastName, entity.Email);

                return user;
            }

            error = "User does not exist!";

            return (null, error);
        }

        public async Task<IEnumerable<UserEntity>> FindAsync(
                Expression<Func<UserEntity, bool>> predicate = null,
                Func<IQueryable<UserEntity>, IOrderedQueryable<UserEntity>> orderBy = null,
                int? skip = null,
                int? take = null)
        {
            var query = _context.Users.AsNoTracking().AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<Guid> Create(User user)
        {
            var userEntity = new UserEntity()
            {
                ID = user.ID,
                Nickname = user.Nickname,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Messages = new List<PrivateMessageEntity>(),
            };
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.ID;
        }
        public async Task<Guid> Delete(Guid id)
        {
            await _context.Users.Where(x => x.ID == id).ExecuteDeleteAsync();
            return id;
        }

        public async Task<Guid> Update(Guid id, string name, string lastname, string nickname, string email)
        {
            await _context.Users
                .Where(x => x.ID == id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(z => z.Name, name)
                    .SetProperty(z => z.LastName, lastname)
                    .SetProperty(z => z.Nickname, nickname)
                    .SetProperty(z => z.Email, email)
            );

            return id;
        }

    }
}

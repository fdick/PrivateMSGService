using Microsoft.EntityFrameworkCore;
using PrivateMSGService.Core.Abstractions;
using PrivateMSGService.Core.Models;
using PrivateMSGService.DataAccess.Entities;
using System.Linq.Expressions;

namespace PrivateMSGService.DataAccess.Repositories
{
    public class PrivateMessageRepository : IPrivateMessageRepository
    {
        private readonly PrivateMSGDbContext _context;

        public PrivateMessageRepository(PrivateMSGDbContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<List<(PrivateMessage, string)>> GetAllAsync()
        {
            var entities = await FindAsync();

            var msges = entities.Select(x => PrivateMessage.Create(x.ID, x.FromUserID, x.ToUserID, x.Message, x.SentTime)).ToList();

            return msges;
        }

        public async Task<(PrivateMessage, string)> GetOneAsync(Guid id)
        {
            var entities = await FindAsync(predicate: x => x.ID == id);
            var entity = entities.FirstOrDefault();

            var error = string.Empty;
            (PrivateMessage, string) msg;

            if (entity != null)
            {
                msg = PrivateMessage.Create(entity.ID, entity.FromUserID, entity.ToUserID, entity.Message, entity.SentTime);

                return msg;
            }

            error = "Message does not exist!";

            return (null, error);
        }

        public async Task<IEnumerable<PrivateMessageEntity>> FindAsync(
                Expression<Func<PrivateMessageEntity, bool>> predicate = null,
                Func<IQueryable<PrivateMessageEntity>, IOrderedQueryable<PrivateMessageEntity>> orderBy = null,
                int? skip = null,
                int? take = null)
        {
            var query = _context.PrivateMessages.AsNoTracking().AsQueryable();

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

        public async Task<Guid> CreateAsync(PrivateMessage msg)
        {
            var entity = new PrivateMessageEntity()
            {
                ID = msg.ID,
                FromUserID = msg.FromUserID,
                ToUserID = msg.ToUserID,
                Message = msg.Message,
                SentTime = msg.SentTime,
            };

            await _context.PrivateMessages.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.ID;
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            await _context.PrivateMessages.Where(x => x.ID == id).ExecuteDeleteAsync();
            return id;
        }

        public async Task<Guid> UpdateAsync(Guid Id, Guid toUserId, Guid fromUserId, string message, DateTime sentTime)
        {
            await _context.PrivateMessages
                .Where(x => x.ID == Id)
                .ExecuteUpdateAsync(x => x.
                    SetProperty(y => y.ToUserID, toUserId).
                    SetProperty(y => y.FromUserID, fromUserId).
                    SetProperty(y => y.Message, message).
                    SetProperty(y => y.SentTime, sentTime)
            );

            return Id;
        }





    }
}

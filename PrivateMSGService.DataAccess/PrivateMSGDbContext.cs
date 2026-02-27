using Microsoft.EntityFrameworkCore;
using PrivateMSGService.DataAccess.Entities;

namespace PrivateMSGService.DataAccess
{
    public class PrivateMSGDbContext : DbContext
    {
        public PrivateMSGDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PrivateMessageEntity> PrivateMessages { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateMSGService.DataAccess.Entities;

namespace PrivateMSGService.DataAccess.Configurations
{
    public class PrivateMessageConfiguration : IEntityTypeConfiguration<PrivateMessageEntity>
    {
        public void Configure(EntityTypeBuilder<PrivateMessageEntity> builder)
        {
            builder.HasKey(x => x.ID);

            builder.Property(x => x.Message)
                .IsRequired();

            builder.HasOne(x => x.FromUser)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.FromUserID);

            builder.Property(x => x.ToUserID).IsRequired();

            builder.Property(x => x.SentTime).IsRequired();

        }
    }
}

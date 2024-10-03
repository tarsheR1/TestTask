using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.ServerApp.DataAccess.Entities;

namespace WebApplication1.ServerApp.DataAccess.Configuration
{
    public class UserEventConfiguration : IEntityTypeConfiguration<UserEventEntity>
    {
        public void Configure(EntityTypeBuilder<UserEventEntity> builder)
        {
            builder.HasKey(u => new { u.UserId, u.EventId });
        }
    }
}

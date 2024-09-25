using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess.Entities;

namespace WebApplication1.Сore.Controllers
{
    public class UserEventConfiguration : IEntityTypeConfiguration<UserEventEntity>
    {
        public void Configure(EntityTypeBuilder<UserEventEntity> builder)
        {
            builder.HasKey(u => new { u.UserId, u.EventId });
        }
    }
}

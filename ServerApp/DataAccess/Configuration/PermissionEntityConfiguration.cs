using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Enums_core_;
using WebApplication1.ServerApp.DataAccess.Entities;

namespace WebApplication1.ServerApp.DataAccess.Configuration
{
    public class PermissionEntityConfiguration : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            var permissions = Enum
                .GetValues(typeof(Permissions))
                .Cast<Permissions>()
                .Select(p => new PermissionEntity
                {
                    Id = (int)p,
                    Name = p.ToString()
                });

            builder.HasData(permissions);
        }
    }

}

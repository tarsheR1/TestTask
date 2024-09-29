using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Eventing.Reader;
using WebApplication1.ServerApp.DataAccess.Entities;
using WebApplication1.Enums_core_;
using WebApplication1.ServerApp.Сore.Models;

namespace WebApplication1.ServerApp.DataAccess.Configuration
{
    public class Configuration : IEntityTypeConfiguration<EventEntity>
    {
        public void Configure(EntityTypeBuilder<EventEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(Event.MAX_TITLE_LENGTH);

            builder.Property(e => e.Location)
                .IsRequired();

            builder.Property(e => e.EventDateTime)
                .IsRequired();
        }
    }
}

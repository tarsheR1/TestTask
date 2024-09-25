using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.Eventing.Reader;
using WebApplication1.DataAccess.Entities;
using WebApplication1.Enums_core_;
using WebApplication1.Сore.Models;

namespace WebApplication1.DataAccess.Configuration
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

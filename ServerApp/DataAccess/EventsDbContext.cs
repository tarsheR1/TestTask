using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using WebApplication1.Models;
using WebApplication1.DataAccess.Entities;
using Microsoft.Extensions.Options;
using WebApplication1.DataAccess.Configuration;
using WebApplication1.Infrastructure.Authorization;

namespace WebApplication1.DataAccess
{
    public class EventsDbContext : DbContext
    {
        private readonly IOptions<AuthorizationOptions> _authOptions;

        public EventsDbContext(DbContextOptions<EventsDbContext> options, IOptions<AuthorizationOptions> authOptions)
            : base(options)
        {
            _authOptions = authOptions;
        }

        public DbSet<EventEntity> Events => Set<EventEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<RoleEntity> Roles => Set<RoleEntity>();
        public DbSet<RolePermissionEntity> RolePermissions => Set<RolePermissionEntity>();
        public DbSet<UserEventEntity> EventParticipants => Set<UserEventEntity>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=TIME_MACHINE\\SQLEXPRESS;Database=Менеджер мероприятий;Trusted_Connection=True;TrustServerCertificate=true;").LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventsDbContext).Assembly);
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(_authOptions.Value));
        }
    } 
}

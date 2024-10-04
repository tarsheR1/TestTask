using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using WebApplication1.ServerApp.Сore.Models;
using WebApplication1.ServerApp.DataAccess.Entities;
using Microsoft.Extensions.Options;
using WebApplication1.ServerApp.DataAccess.Configuration;
using WebApplication1.ServerApp.Infrastructure.Authorization;

namespace WebApplication1.ServerApp.DataAccess
{
    public class EventsDbContext : DbContext
    {
        private readonly IOptions<AuthorizationOptions> _authOptions;
        private readonly IConfiguration _configuration;
        public EventsDbContext(
            DbContextOptions<EventsDbContext> options, 
            IOptions<AuthorizationOptions> authOptions,
            IConfiguration configuration)
            : base(options)
        {
            _authOptions = authOptions;
            _configuration = configuration;
        }

        public DbSet<EventEntity> Events => Set<EventEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<RoleEntity> Roles => Set<RoleEntity>();
        public DbSet<RolePermissionEntity> RolePermissions => Set<RolePermissionEntity>();
        public DbSet<UserEventEntity> EventParticipants => Set<UserEventEntity>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString).LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventsDbContext).Assembly);
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(_authOptions.Value));
        }
    }
}

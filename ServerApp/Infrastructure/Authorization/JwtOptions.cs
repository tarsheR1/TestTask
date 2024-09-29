namespace WebApplication1.ServerApp.Infrastructure.Authorization
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;

        public int ExpiresHours { get; set; }
    }
}

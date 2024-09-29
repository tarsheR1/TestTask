using Microsoft.EntityFrameworkCore;
using WebApplication1.ServerApp.Сore.Interfaces;
using React.AspNet;
using WebApplication1.ServerApp.Infrastructure.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using WebApplication1.Enums_core_;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.ServerApp.Application.Services;
using WebApplication1.ServerApp.DataAccess;
using WebApplication1.ServerApp.Infrastructure.Authorization;
using WebApplication1.ServerApp.Infrastructure.Authorization.PasswordHasher;
using WebApplication1.ServerApp.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Настройка аутентификации
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter your token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddReact();

builder.Services.AddDbContext<EventsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(EventsDbContext)));
});

builder.Services.Configure<WebApplication1.ServerApp.Infrastructure.Authorization.AuthorizationOptions>(builder.Configuration.GetSection(nameof(WebApplication1.ServerApp.Infrastructure.Authorization.AuthorizationOptions)));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();  

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IEventsRepository, EventsRepository>();
builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddAutoMapper(typeof(UserEntityToUserProfile).Assembly);
builder.Services.AddAutoMapper(typeof(UserToUserEntityProfile).Assembly);
builder.Services.AddAutoMapper(typeof(EventToEventEntity).Assembly);
builder.Services.AddAutoMapper(typeof(EventEntityToEvent).Assembly);
var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    { 
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["Titul"];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireUserPermission", policy =>
        policy.Requirements.Add(new PermissionRequirment(new[] { Permissions.Read })));

    options.AddPolicy("RequireAdminPermission", policy =>
        policy.Requirements.Add(new PermissionRequirment(new[] { Permissions.Read, Permissions.Create, Permissions.Delete, Permissions.Update })));
});

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new UserEntityToUserProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseReact(config => { });
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "EventsWebApi documentation";
});

app.MapControllers();

app.Run();

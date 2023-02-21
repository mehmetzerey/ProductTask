using App.Application.Authentication;
using App.Application.Repositories.Products;
using App.Application.Repositories.Tickets;
using App.Persistence.Authentication;
using App.Persistence.Repositories.Products;
using App.Persistence.Repositories.Tickets;
using Microsoft.Extensions.Configuration;

namespace App.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppIdentityDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DbConnectionString")));

        // rol bazlı attribute lar çalışsın diye eklendi
        services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

        string secretStr = configuration["Application:Secret"];
        byte[] secret = Encoding.UTF8.GetBytes(secretStr);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Audience = configuration["Application:Audience"];
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(secret),
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = configuration["Application:Issuer"],
                ValidAudience = configuration["Application:Audience"],
            ClockSkew = TimeSpan.FromHours(3),
            };
        });

        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IReadTicketRepository, ReadTicketRepository>();
        services.AddScoped<IWriteTicketRepository, WriteTicketRepository>();

        services.AddScoped<IReadProductRepository, ReadProductRepository>();
        services.AddScoped<IWriteProductRepository, WriteProductRepository>();

        services.AddScoped<IReadUserRepository, ReadUserRepository>();
        services.AddScoped<IWriteUserRepository, WriteUserRepository>();

        services.AddScoped<IWriteRoleRepository, WriteRoleRepository>();

        return services;
    }
}

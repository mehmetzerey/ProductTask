namespace App.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityServerConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString")));

        // rol bazlı attribute lar çalışsın diye eklendi
        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
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
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(secret),
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };
        });


        return services;
    }

}

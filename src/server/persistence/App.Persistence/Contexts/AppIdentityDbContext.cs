namespace App.Persistence.Contexts;

public class AppIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options)
    {
    }
    public DbSet<ApplicationUserTokens> ApplicationUserTokens { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new IdentityRoleEntityTypeConfiguration());
        base.OnModelCreating(builder);
    }
}

namespace App.Persistence.Repositories.User;

public class ReadUserRepository : ReadRepository<ApplicationUser>, IReadUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    public ReadUserRepository(AppIdentityDbContext appIdentityDbContext, UserManager<ApplicationUser> userManager) : base(appIdentityDbContext)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser> FindByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            return user;
        }
        else
        {
            return new ApplicationUser();
        }
    }

    public Task<ApplicationUser> FindByIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationUser> FindByNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public override Task<ApplicationUser> GetByIdAsync(Guid Id)
    {
        return base.GetByIdAsync(Id);
    }

    public override IQueryable<ApplicationUser> GetWhere(Expression<Func<ApplicationUser, bool>> method, bool tracking = true)
    {
        return base.GetWhere(method, tracking);
    }
}


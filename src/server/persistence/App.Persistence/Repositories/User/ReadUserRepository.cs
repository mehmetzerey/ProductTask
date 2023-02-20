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
            return null;
        }
    }

    public async Task<ApplicationUser> FindByIdAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user != null)
        {
            return user;
        }
        else
        {
            return null;
        }
    }

    public async Task<ApplicationUser> FindByNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public List<ApplicationUser> GetAll()
    {
        return base.GetAll().ToList();
    }

    public override Task<ApplicationUser> GetByIdAsync(Guid Id)
    {
        return base.GetByIdAsync(Id);
    }

    public async Task<List<ApplicationUser>> GetUserByRole(int roleId)
    {
        var roleEnum = RoleEnum.From(roleId).Name;
        var getUsersByRole = await _userManager.GetUsersInRoleAsync(roleEnum);
        return getUsersByRole.ToList();
    }

    public override IQueryable<ApplicationUser> GetWhere(Expression<Func<ApplicationUser, bool>> method, bool tracking = true)
    {
        return base.GetWhere(method, tracking);
    }
}


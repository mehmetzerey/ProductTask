namespace App.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : class
{
    public Guid Id { get; set; }

    private readonly AppIdentityDbContext _appIdentityDbContext;

    public ReadRepository(AppIdentityDbContext panelJantAPIDbContext)
    {
        _appIdentityDbContext = panelJantAPIDbContext;
    }
    public virtual DbSet<T> Table => _appIdentityDbContext.Set<T>();

    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }

    public virtual IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.Where(method);
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }

    public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(method);
    }

    public virtual async Task<T> GetByIdAsync(Guid Id) => await Table.FindAsync(Id);
}


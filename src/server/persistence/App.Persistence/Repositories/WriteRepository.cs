namespace App.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : class
{
    readonly private AppIdentityDbContext _appIdentityDbContext;

    public WriteRepository(AppIdentityDbContext appIdentity)
    {
        _appIdentityDbContext = appIdentity;
    }

    public virtual DbSet<T> Table => _appIdentityDbContext.Set<T>();

    public virtual async Task<bool> AddAsync(T model)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(model);
        return entityEntry.State == EntityState.Added;
    }

    public virtual async Task<bool> AddRangeAsync(List<T> model)
    {
        await Table.AddRangeAsync(model);
        return true;
    }

    public virtual bool Remove(T model)
    {
        EntityEntry<T> entityEntry = Table.Remove(model);
        return entityEntry.State == EntityState.Deleted;
    }

    public virtual async Task<bool> RemoveAsync(Guid Id)
    {
        T model = await Table.FindAsync(Id);
        return Remove(model);
    }

    public virtual bool Update(T model)
    {
        EntityEntry<T> entityEntry = Table.Update(model);
        return entityEntry.State == EntityState.Modified;
    }

    public async Task<int> SaveAsync() => await _appIdentityDbContext.SaveChangesAsync();

}

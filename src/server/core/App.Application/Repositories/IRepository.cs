namespace App.Application.Repositories;

public interface IRepository<T> where T : class
{
    DbSet<T> Table { get; }
}

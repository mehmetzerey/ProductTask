namespace App.Application.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : class
{
    Task<int> SaveAsync();
}

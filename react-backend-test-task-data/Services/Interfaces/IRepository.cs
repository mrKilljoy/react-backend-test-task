namespace react_backend_test_task_data.Services.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> Get(Guid id);

    Task<T> Create(T entry);
}
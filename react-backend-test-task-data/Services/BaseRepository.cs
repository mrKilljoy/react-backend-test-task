using Microsoft.EntityFrameworkCore;
using react_backend_test_task_data.Models;
using react_backend_test_task_data.Services.Interfaces;

namespace react_backend_test_task_data.Services;

public abstract class BaseRepository<T> : IRepository<T> where T : BaseModel
{
    protected readonly TestDbContext _dbContext;

    protected BaseRepository(TestDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<T?> Get(Guid id)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public virtual async Task<T> Create(T entry)
    {
        var result = await _dbContext.Set<T>().AddAsync(entry);

        return result.Entity;
    }
}
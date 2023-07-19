using Microsoft.EntityFrameworkCore;
using react_backend_test_task_data.Models;
using react_backend_test_task_data.Services.Interfaces;

namespace react_backend_test_task_data.Services;

public class TreeRepository : BaseRepository<Tree>, ITreeRepository
{
    public TreeRepository(TestDbContext dbContext): base(dbContext) { }

    public async Task<Tree?> Get(string name)
    {
        return await _dbContext.Trees
            .Include(x => x.Nodes)
            .FirstOrDefaultAsync(x => x.Name == name);
    }
}
using react_backend_test_task_data.Models;

namespace react_backend_test_task_data.Services.Interfaces;

public interface ITreeRepository : IRepository<Tree>
{
    Task<Tree?> Get(string name);
}
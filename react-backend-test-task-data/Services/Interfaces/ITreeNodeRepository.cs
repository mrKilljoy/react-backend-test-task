using react_backend_test_task_data.Models;

namespace react_backend_test_task_data.Services.Interfaces;

public interface ITreeNodeRepository : IRepository<TreeNode>
{
    Task Delete(Guid treeId, Guid nodeId);

    Task<TreeNode> Rename(Guid nodeId, string newName);
}
using Microsoft.EntityFrameworkCore;
using react_backend_test_task_data.Exceptions;
using react_backend_test_task_data.Models;
using react_backend_test_task_data.Services.Interfaces;

namespace react_backend_test_task_data.Services;

public class TreeNodeRepository : BaseRepository<TreeNode>, ITreeNodeRepository
{
    public TreeNodeRepository(TestDbContext dbContext) : base(dbContext) { }

    public async Task Delete(Guid treeId, Guid nodeId)
    {
        if (await _dbContext.TreeNodes.AnyAsync(x => x.ParentId == nodeId))
        {
            throw new SecureException("Child nodes must be deleted first");
        }

        var item = await _dbContext.TreeNodes.FirstOrDefaultAsync(x => x.Id == nodeId && x.TreeId == treeId);
        if (item is null)
        {
            throw new SecureException("Node not found");
        }
        
        if (!item.ParentId.HasValue)
        {
            throw new SecureException("Cannot delete the root node");
        }

        _dbContext.TreeNodes.Remove(item);
    }

    public async Task<TreeNode> Rename(Guid nodeId, string newName)
    {
        var entry = await Get(nodeId);
        if (entry is null)
        {
            throw new SecureException("Node not found");
        }

        entry.Name = newName;

        var result = _dbContext.TreeNodes.Update(entry);

        return result.Entity;
    }

    public override async Task<TreeNode> Create(TreeNode entry)
    {
        if (!await _dbContext.Trees.AnyAsync(x => x.Id == entry.TreeId))
        {
            throw new SecureException("Specified tree not found");
        }
        
        if (!entry.ParentId.HasValue || !await _dbContext.TreeNodes.AnyAsync(x => x.Id == entry.ParentId))
        {
            throw new SecureException("No valid parent node found");
        }

        var result = await _dbContext.TreeNodes.AddAsync(entry);

        return result.Entity;
    }
}
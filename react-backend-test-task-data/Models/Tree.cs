namespace react_backend_test_task_data.Models;

public class Tree : BaseModel
{
    public string Name { get; set; }

    public ICollection<TreeNode> Nodes { get; set; }
}
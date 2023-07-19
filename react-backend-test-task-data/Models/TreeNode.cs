namespace react_backend_test_task_data.Models;

public class TreeNode : BaseModel
{
    public string Name { get; set; }

    public Guid? ParentId { get; set; }

    public Guid TreeId { get; set; }
}
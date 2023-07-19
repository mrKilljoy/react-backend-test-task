namespace react_backend_test_task_data.Models;

public class JournalEntry : BaseModel
{
    public string Data { get; set; }

    public string Type { get; set; }

    public DateTime CreatedAt { get; set; }
}
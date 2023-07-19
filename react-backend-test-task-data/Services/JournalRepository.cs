using react_backend_test_task_data.Models;
using react_backend_test_task_data.Services.Interfaces;

namespace react_backend_test_task_data.Services;

public class JournalRepository : BaseRepository<JournalEntry>, IJournalRepository
{
    public JournalRepository(TestDbContext dbContext) : base(dbContext) { }
}
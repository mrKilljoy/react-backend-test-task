using Microsoft.AspNetCore.Mvc.Filters;
using react_backend_test_task_data;

namespace react_backend_test_task.Infrastructure;

public class SaveDatabaseChangesFilter : IAsyncActionFilter
{
    private readonly TestDbContext _dbContext;

    public SaveDatabaseChangesFilter(TestDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var result = await next();

        if (result.Exception is null || result.ExceptionHandled)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
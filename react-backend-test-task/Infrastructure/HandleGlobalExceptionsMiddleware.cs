using System.Net;
using react_backend_test_task_data;
using react_backend_test_task_data.Models;
using react_backend_test_task.Models;

namespace react_backend_test_task.Infrastructure;

public class HandleGlobalExceptionsMiddleware
{
    private readonly RequestDelegate _next;

    public HandleGlobalExceptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext, TestDbContext dbContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var journalEntry = await CreateJournalEntry(dbContext, ex);
            
            await WriteErrorResponse(httpContext, journalEntry);
        }
    }

    private async Task<JournalEntry?> CreateJournalEntry(TestDbContext dbContext, Exception exception)
    {
        return await Task.Run(async () =>
        {
            var entry = await dbContext.JournalEntries.AddAsync(new JournalEntry
            {
                CreatedAt = DateTime.UtcNow,
                Type = exception.GetType().Name,
                Data = exception.ToString()
            });
            
            await dbContext.SaveChangesAsync();
            return entry.Entity;
        }).ContinueWith(t => t.Status == TaskStatus.RanToCompletion && t.Exception is null ? t.Result : null);
    }

    private async Task WriteErrorResponse(HttpContext context, JournalEntry? journalEntry)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var errorItem = new ErrorMessage
        {
            Id = journalEntry?.Id ?? Guid.Empty,
            Type = journalEntry?.Type ?? nameof(Exception),
            Data = new ErrorMessage.ErrorData
            {
                Message = journalEntry?.Data ?? "An unexpected error has occurred"
            },
        };
        await context.Response.WriteAsync(errorItem.ToString()!);
    }
}
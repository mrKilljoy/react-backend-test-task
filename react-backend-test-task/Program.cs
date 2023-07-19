using System.Reflection;
using Microsoft.EntityFrameworkCore;
using react_backend_test_task_data;
using react_backend_test_task_data.Services;
using react_backend_test_task_data.Services.Interfaces;
using react_backend_test_task.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
    string filePath = Path.Combine(AppContext.BaseDirectory, assemblyName + ".xml");
    options.IncludeXmlComments(filePath);
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMvcCore(options =>
{
    options.Filters.Add<SaveDatabaseChangesFilter>();
});

builder.Services.AddDbContext<TestDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PgConnectionString")));

builder.Services.AddTransient<ITreeRepository, TreeRepository>();
builder.Services.AddTransient<ITreeNodeRepository, TreeNodeRepository>();
builder.Services.AddTransient<IJournalRepository, JournalRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<HandleGlobalExceptionsMiddleware>();

app.Run();
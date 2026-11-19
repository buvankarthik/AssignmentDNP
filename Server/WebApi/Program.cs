using EfcRepositories;
using RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IPostRepository, EfcPostRepository>();
builder.Services.AddScoped<ICommentRepository, EfcCommentRepository>();
builder.Services.AddScoped<IUserRepository, EfcUserRepository>();
builder.Services.AddDbContext<EfcRepositories.AppContext>();


var app = builder.Build();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (InvalidOperationException e)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsync(e.Message);
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
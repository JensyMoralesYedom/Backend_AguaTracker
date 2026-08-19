using Backend_AguaTracker.Repository;
using Backend_AguaTracker.Service;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRepository(builder.Configuration);
builder.Services.AddService(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

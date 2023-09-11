using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TascApp.Models;

var builder = WebApplication.CreateBuilder(args);

//Add config for database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Add controllers
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<TaskContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddSwaggerGen(controller =>
{
    controller.SwaggerDoc("v1", new() { Title = "TaskApp", Version = "v1" });
});

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

app.Run();

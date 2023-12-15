using Cube_C___API.Repositories;
using Microsoft.EntityFrameworkCore;
using AppContext = Cube_C___API.AppContext;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<AppContext>(options => options.UseMySQL(Environment.GetEnvironmentVariable("connectionString")));
builder.Services.AddScoped<UsersRepository>();
builder.Services.AddScoped<CustomersRepository>();



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
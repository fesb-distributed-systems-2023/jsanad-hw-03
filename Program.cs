using CompanyApplication.Logic;
using CompanyApplication.Repositories;
using Microsoft.Data.Sqlite;
using DIS_project.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICompanyLogic, CompanyLogic>();
builder.Services.AddSingleton<ICompanyRepository, CompanyRepository_SQL>();
builder.Services.Configure<ValidationConfiguration>(builder.Configuration.GetSection("ValidationConfiguration"));
builder.Services.Configure<DBConfiguration>(builder.Configuration.GetSection("Database"));

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors("cors_policy_allow_all");

app.MapControllers();

app.Run();

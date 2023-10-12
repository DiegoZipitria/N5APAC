using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PAC.BusinessLogic;
using PAC.DataAccess;
using PAC.Domain;
using PAC.IBusinessLogic;
using PAC.IDataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStudentsRepository<Student>, StudentsRepository<Student>>();

builder.Services.AddScoped<IStudentLogic, StudentLogic>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
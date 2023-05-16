using Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using SeniorTest.WeatherAPI;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddDbContext<AssessmentsContext>(options => options.UseSqlServer("Data Source = 172.20.12.239; User ID = archive; Password = zadA7uVa!!!; Connect Timeout = 30; Database = Assessments; Encrypt = False; Trust Server Certificate=False; Application Intent = ReadWrite; Multi Subnet Failover=False;"));
builder.Services.AddMvc();

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

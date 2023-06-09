using Microsoft.EntityFrameworkCore;
using Moreno.RPA_AeC.Infra.CrossCutting.IoC.Configurations;
using Moreno.RPA_AeC.Infra.Data.Context.Entity;
using log4net.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ResolveDependencies();
builder.Services.AddDbContext<RPA_AeCDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Logging.AddLog4Net();
XmlConfigurator.Configure(new FileInfo("log4net.config"));

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

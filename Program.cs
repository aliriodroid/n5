using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using N5User.Commands;
using N5User.Data.Context;
using N5User.Data.Models;
using N5User.Data.Repositories;
using N5User.Data.UnitOfWork;
using N5User.Services;
using Serilog;
using System;
using System.Text.Json.Serialization;
using Nest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//
// builder.Services.AddControllers().AddJsonOptions(x =>
//     x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var settings = new ConnectionSettings(new Uri("http://elasticsearch:9200")).DefaultIndex("n5");
var client = new ElasticClient(settings);
builder.Services.AddSingleton(client);

builder.Services.AddDbContext<DataContext>(options =>{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddValidatorsFromAssemblyContaining<Permission>(ServiceLifetime.Transient);
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IPermissionRepository,PermissionRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddSingleton<MessageService>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
builder.Host.UseSerilog();
//builder.Services.AddHostedService<MessageReceiver>(); 
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

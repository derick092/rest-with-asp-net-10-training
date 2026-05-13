using RWAN10T.Api.Configurations;
using RWAN10T.Api.Repositories;
using RWAN10T.Api.Repositories.Impl;
using RWAN10T.Api.Services;
using RWAN10T.Api.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddSerilogLogging();

builder.Services.AddControllers();

builder.Services.AddDataBaseConfiguration(builder.Configuration);

builder.Services.AddEvolveConfiguration(builder.Configuration, builder.Environment);

builder.Services.AddScoped<IPersonServices, PersonServicesImpl>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImpl>();

builder.Services.AddScoped<IBookService, BookServicesImpl>();
builder.Services.AddScoped<IBookRepository, BookRepositoryImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

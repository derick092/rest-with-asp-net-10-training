using RWAN10T.Api.Configurations;
using RWAN10T.Api.Repositories;
using RWAN10T.Api.Services;
using RWAN10T.Api.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddSerilogLogging();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenAPIConfig();
builder.Services.AddSwaggerConfig();

builder.Services.AddControllers().AddContentNegotiation();

builder.Services.AddDataBaseConfiguration(builder.Configuration);

builder.Services.AddEvolveConfiguration(builder.Configuration, builder.Environment);

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.Configure<RouteOptions>(options => 
{ options.LowercaseUrls = true; options.LowercaseQueryStrings = true; });

builder.Services.AddScoped<IPersonServices, PersonServicesImpl>();
builder.Services.AddScoped<PersonServicesImplV2>();
builder.Services.AddScoped<IBookService, BookServicesImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerSpecification();
app.UseScalarConfiguration();

app.Run();

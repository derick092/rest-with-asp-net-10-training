using RWAN10T.Api.Configurations;
using RWAN10T.Api.Hypermdia.Filters;
using RWAN10T.Api.Repositories;
using RWAN10T.Api.Services;
using RWAN10T.Api.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddSerilogLogging();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenAPIConfig();
builder.Services.AddSwaggerConfig();

builder.Services.AddControllers(options => 
{
    options.Filters.Add<HypermediaFilter>();
}).AddContentNegotiation();

builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddHATEOASConfiguration();

builder.Services.AddDataBaseConfiguration(builder.Configuration);

builder.Services.AddEvolveConfiguration(builder.Configuration, builder.Environment);

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.Configure<RouteOptions>(options => 
{ options.LowercaseUrls = true; options.LowercaseQueryStrings = true; });

builder.Services.AddScoped<IPersonServices, PersonServicesImpl>();
builder.Services.AddScoped<PersonServicesImplV2>();
builder.Services.AddScoped<IBookService, BookServicesImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();
app.UseCorsConfiguration(builder.Configuration);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHATEOASRoutes();

app.UseSwaggerSpecification();
app.UseScalarConfiguration();


app.Run();

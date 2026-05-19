using RWAN10T.Api.Configurations;
using RWAN10T.Api.Files.Exporters.Factory;
using RWAN10T.Api.Files.Exporters.Impl;
using RWAN10T.Api.Files.Importers.Factory;
using RWAN10T.Api.Files.Importers.Impl;
using RWAN10T.Api.Hypermdia.Filters;
using RWAN10T.Api.Mail;
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
builder.Services.AddEmailConfiguration(builder.Configuration);

builder.Services.AddDataBaseConfiguration(builder.Configuration);

builder.Services.AddEvolveConfiguration(builder.Configuration, builder.Environment);

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.Configure<RouteOptions>(options => 
{ options.LowercaseUrls = true; options.LowercaseQueryStrings = true; });

builder.Services.AddScoped<IPersonServices, PersonServicesImpl>();
builder.Services.AddScoped<PersonServicesImplV2>();
builder.Services.AddScoped<IBookService, BookServicesImpl>();

builder.Services.AddSingleton<HttpContextAccessor>();
builder.Services.AddScoped<IFileServices, FileServicesImpl>();

builder.Services.AddScoped<CsvFileImporter>();
builder.Services.AddScoped<ExcelFileImporter>();
builder.Services.AddScoped<FileImporterFactory>();

builder.Services.AddScoped<CsvExporter>();
builder.Services.AddScoped<ExcelExporter>();
builder.Services.AddScoped<FileExporterFactory>();

builder.Services.AddScoped<IEmailService, EmailServiceImpl>();
builder.Services.AddScoped<EmailSender>();

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

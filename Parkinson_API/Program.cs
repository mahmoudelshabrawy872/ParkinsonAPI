using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Parkinson_API.Helpers.AutoMapper;
using Parkinson_DataAccess.Data;
using Parkinson_DataAccess.Repository;
using Parkinson_DataAccess.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options
    .UseSqlServer(builder
    .Configuration
    .GetConnectionString("Default")));
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services.AddScoped<IUniteOfWork, UniteOfWork>();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddApiVersioning(option =>
{
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.DefaultApiVersion = new ApiVersion(1, 0);
    option.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(option =>
{
    option.GroupNameFormat = "'v'VVV";
    option.SubstituteApiVersionInUrl = true;
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1.0",
        Title = "Parkinson",
        Description = "API For Parkinson Mobile Application V1",
    });
    options.SwaggerDoc("v2", new OpenApiInfo()
    {
        Version = "v2.0",
        Title = "Parkinson",
        Description = "API For Parkinson Mobile Application V2",
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Parkinson_APIV1");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "Parkinson_APIV2");
});
//}
app.Map("/", () => Results.Redirect("/swagger"));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

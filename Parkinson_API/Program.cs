using Microsoft.EntityFrameworkCore;
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


builder.Services.AddScoped<IUniteOfWork, UniteOfWork>();

builder.Services.AddAutoMapper(typeof(MappingConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}
app.Map("/", () => Results.Redirect("/swagger"));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

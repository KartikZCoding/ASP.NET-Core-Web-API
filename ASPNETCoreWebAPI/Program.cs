using ASPNETCoreWebAPI.Configurations;
using ASPNETCoreWebAPI.Data;
using ASPNETCoreWebAPI.Data.Repository;
using ASPNETCoreWebAPI.EFDBFirst;
using ASPNETCoreWebAPI.MyLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMyLogger, LogToMemoryServer>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped(typeof(ICollegeRepository<>), typeof(CollegeRepository<>));

//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Information()
//    .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Minute)
//    .CreateLogger();

//use this line to override the built-in logger
//builder.Host.UseSerilog();

//use serilog along with built-in logger
//builder.Logging.AddSerilog();

builder.Services.AddDbContext<CollegeDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeAppDBConnection"));
});
builder.Services.AddDbContext<NorthwindContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EFDBFirstDBConnection"));
});

builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperConfig));

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

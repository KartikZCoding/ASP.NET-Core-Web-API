using ASPNETCoreWebAPI.Configurations;
using ASPNETCoreWebAPI.Data;
using ASPNETCoreWebAPI.Data.Repository;
using ASPNETCoreWebAPI.EFDBFirst;
using ASPNETCoreWebAPI.MyLogging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
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

//Database Connection string
builder.Services.AddDbContext<CollegeDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeAppDBConnection"));
});
builder.Services.AddDbContext<NorthwindContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EFDBFirstDBConnection"));
});

//AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperConfig));

//CORS
builder.Services.AddCors(options =>
{
    //options.AddDefaultPolicy(policy =>
    //{
    //    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    //});

    options.AddPolicy("AllowAll", policy =>
    {
        //for all origin
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
    options.AddPolicy("AllowOnlyLocalhost", policy =>
    {
        //for specific origin
        policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
    });
    options.AddPolicy("AllowOnlyGoogle", policy =>
    {
        //for specific origin
        policy.WithOrigins("http://google.com","http://gmail.com","http://drive.google.com").AllowAnyHeader().AllowAnyMethod();
    });
    options.AddPolicy("AllowOnlyMicrosoft", policy =>
    {
        //for specific origin
        policy.WithOrigins("http://outlook.com","http://microsoft.com","http://onedrive.com").AllowAnyHeader().AllowAnyMethod();
    });
});

var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecret"));

//Add Authentication Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    //but make it false in production environment
    //options.RequireHttpsMetadata = false;

    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        // validate the signing key
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience =false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAll");
app.UseAuthorization();

app.UseEndpoints(endpoits =>
{
    endpoits.MapGet("api/testingendpoint",
        context => context.Response.WriteAsync("Test Response"))
    .RequireCors("AllowOnlyLocalhost");

    endpoits.MapControllers().RequireCors("AllowAll");

    endpoits.MapGet("api/testingendpoint1",
        context => context.Response.WriteAsync("Test response 1"));
        //context => context.Response.WriteAsync(builder.Configuration.GetValue<string>("JWTSecret")));
});
//app.MapControllers();

app.Run();

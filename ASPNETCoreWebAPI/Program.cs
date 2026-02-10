using ASPNETCoreWebAPI.Configurations;
using ASPNETCoreWebAPI.Data;
using ASPNETCoreWebAPI.Data.Repository;
using ASPNETCoreWebAPI.EFDBFirst;
using ASPNETCoreWebAPI.MyLogging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the bearer scheme. Enter Bearer [space] add your token in the text inout. Example: Bearer $#&*@&DJHWWaihauhfu...",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    }); 
});

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

//var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecret"));

//Add Authentication Configuration (Default)
/*builder.Services.AddAuthentication(options =>
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
});*/

var keyGoogle = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretForGoogle"));
var keyMicrosoft = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretForMicrosoft"));
var keyLocal = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretForLocal"));
string GoogleAudience = builder.Configuration.GetValue<string>("GoogleAudience");
string MicrosoftAudience = builder.Configuration.GetValue<string>("MicrosoftAudience");
string LocalAudience = builder.Configuration.GetValue<string>("LocalAudience");
string GoogleIssuer = builder.Configuration.GetValue<string>("GoogleIssuer");
string MicrosoftIssuer = builder.Configuration.GetValue<string>("MicrosoftIssuer");
string LocalIssuer = builder.Configuration.GetValue<string>("LocalIssuer");

//Add Authentication Configuration (Named)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("LoginForGoogleUsers", options =>
{
    //but make it false in production environment
    //options.RequireHttpsMetadata = false;

    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        // validate the signing key
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyGoogle),

        ValidateIssuer = true,
        ValidIssuer = GoogleIssuer,

        ValidateAudience = true,
        ValidAudience = GoogleAudience,
    };
})
.AddJwtBearer("LoginForMicrosoftUsers", options =>
{
    //but make it false in production environment
    //options.RequireHttpsMetadata = false;

    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        // validate the signing key
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyMicrosoft),

        ValidateIssuer = true,
        ValidIssuer = MicrosoftIssuer,

        ValidateAudience = true,
        ValidAudience = MicrosoftAudience,
    };
})
.AddJwtBearer("LoginForLocalUsers", options =>
{
    //but make it false in production environment
    //options.RequireHttpsMetadata = false;

    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        // validate the signing key
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyLocal),

        ValidateIssuer = true,
        ValidIssuer = LocalIssuer,

        ValidateAudience = true,
        ValidAudience = LocalAudience,
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

using System.Reflection;
using System.Text;
using backend.Data;
using backend.Middleware;
using backend.Models.Configuration;
using backend.Services;
using backend.Services.Implementations;
using backend.Wrappers;

using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;

using Microsoft.Extensions.DependencyInjection;


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using MySqlConnector;
using backend.Models.Common;
using backend.Account;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(cfg =>
{
    cfg.DefaultApiVersion = new ApiVersion(1, 0);
    cfg.AssumeDefaultVersionWhenUnspecified = true;
    cfg.ReportApiVersions = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Clean Architecture - WebApi",
        Description = "This Api will be responsible for overall data distribution and authorization.",
        Contact = new OpenApiContact
        {
            Name = "LongLongDragon",
            Email = "adming@lldragon.net",
            Url = new Uri("https://lldragon.net/contact")
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Input your Bearer token in this format - Bearer {your token here} to access this API"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors(options => options.AddDefaultPolicy(corsPolicyBuilder =>
{
    corsPolicyBuilder.AllowAnyOrigin();
    corsPolicyBuilder.AllowAnyMethod();
    corsPolicyBuilder.AllowAnyHeader();
}));

// We get the connection string from the Environment variables
var connectionString = builder.Configuration.GetConnectionString("MariadbIdentity");

// We add the database to the Mariadb identity store
builder.Services.AddDbContext<IdentityContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b => b.SchemaBehavior(MySqlSchemaBehavior.Translate,
    (schema, entity) => $"{schema ?? "dbo"}_{entity}")));
        builder.    Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

// We add the repository and unit of work to the DI container
builder.Services.AddTransient<IUnitOfWork, UnitOfWorkSqlKata>();

// We add fluent migrations to the DI container




// We now load the normal database connection string
var connectionString2 = builder.Configuration.GetConnectionString("Mariadb");

// We connect to database, create the homestats database if it doesn't exist
// We enter with sql client from C#
{
    var connectionString3 = builder.Configuration.GetConnectionString("MariaDbCreation");
using var connection = new MySqlConnection(connectionString3);
connection.Open();
using var command = connection.CreateCommand();
command.CommandText = "CREATE DATABASE IF NOT EXISTS homestats";
command.ExecuteNonQuery();
}

// We run the fluent migrations on startup
builder.Services
.AddFluentMigratorCore()
.ConfigureRunner(rb => rb
    .AddMySql5()
    .WithGlobalConnectionString(connectionString2)
    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
.AddLogging(lb => lb.AddFluentMigratorConsole());

// We run the migrations on startu
{
using var scope = builder.Services.BuildServiceProvider().CreateScope();
var db = scope.ServiceProvider.GetRequiredService<IdentityContext>();
db.Database.Migrate();

var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
runner.MigrateUp();

}
// We configure the authorization service
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin"));
    options.AddPolicy("User", policy => policy.RequireClaim("role", "user"));
});
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
            ValidAudience = builder.Configuration["JWTSettings:Audience"],
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Key"] ?? throw new InvalidOperationException()))
        };
        o.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = c =>
            {
                c.NoResult();
                c.Response.StatusCode = 500;
                c.Response.ContentType = "text/plain";
                return c.Response.WriteAsync(c.Exception.ToString());
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
                return context.Response.WriteAsync(result);
            },
            OnForbidden = context =>
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                var result =
                    JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
                return context.Response.WriteAsync(result);
            }
        };
    })
    ;

// We add the mediator to the DI container
builder.Services.AddMediatR(cfg =>
{
    cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddSingleton<JwtSettings>(_ => builder.Configuration.GetSection("JWTSettings").Get<JwtSettings>() ?? throw new InvalidOperationException());
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IHouseService, HouseService>();
builder.Services.AddTransient<ISensorService, SensorService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
using System.Reflection;
using System.Text;
using backend.Data;
using backend.Middleware;
using backend.Models.Configuration;
using backend.Services;
using backend.Services.Implementations;
using backend.Wrappers;
using LiteDB;
using LiteDB.Identity.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

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

// We add the litedb database to the DI container 
var connectionString = builder.Configuration.GetConnectionString("LiteDb");
// We add a transient service to the DI container
builder.Services.AddTransient<ILiteDatabase>(_ => new LiteDatabase(connectionString));
builder.Services.AddLiteDBIdentity(connectionString).AddDefaultTokenProviders();

// We add the repository and unit of work to the DI container
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

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
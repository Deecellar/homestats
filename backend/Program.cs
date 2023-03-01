using System.Reflection;
using backend.Middleware;
using Microsoft.AspNetCore.Mvc;
using backend.Services.Implementations;
using backend.Services;
using backend.Data;
using MediatR;
using LiteDB;
using backend.Models.Identity;
using Microsoft.AspNetCore.Identity;
using backend.Data.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(cfg => {
    cfg.DefaultApiVersion = new ApiVersion(1, 0);
    cfg.AssumeDefaultVersionWhenUnspecified = true;
    cfg.ReportApiVersions = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
}));

// We add the litedb database to the DI container 
var connectionString = builder.Configuration.GetConnectionString("LiteDb");
// We add a transient service to the DI container
builder.Services.AddTransient<ILiteDatabase>(provider => new LiteDatabase(connectionString));

// We add the repository and unit of work to the DI container
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// We configure the authorization service
// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin"));
//     options.AddPolicy("User", policy => policy.RequireClaim("role", "user"));
// });
// We add idenity core to the DI container
// builder.Services.AddIdentityCore<User>()
// .AddRoles<Role>()
// .AddSignInManager<SignInManager<User>>()
// .AddUserManager<UserManager<User>>()
// .AddRoleManager<RoleManager<Role>>()
// .AddRoleStore<RoleStore>()
// .AddUserStore<UserStore>();

// We add the mediator to the DI container
builder.Services.AddMediatR(cfg => {
    cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});



builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
// builder.Services.AddTransient<IAccountService, AccountService>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();

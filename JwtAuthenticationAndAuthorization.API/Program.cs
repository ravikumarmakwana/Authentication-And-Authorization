using JwtAuthenticationAndAuthorization.API;
using JwtAuthenticationAndAuthorization.API.AppStartup;
using JwtAuthenticationAndAuthorization.Business.Profiles;
using JwtAuthenticationAndAuthorization.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Add AutoMapper
builder.Services.AddAutoMapper(new Assembly[]
{
    Assembly.GetAssembly(type: typeof(UserProfile))
});

// Add Services
builder.Services.AddServices();

// Add Identity Configuration
IdentityConfigurator.Configure(builder.Services);

// Add Authentication Configuration
AuthenticationConfigurator.Configure(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

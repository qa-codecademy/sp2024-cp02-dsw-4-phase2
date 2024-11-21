using DropShipping.DataBase.Context;
using DropShipping.Helpers;
using DropShipping.Shared.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Load configuration for DropShippingAppSettings
var dropShipSettings = builder.Configuration.GetSection("DropShippingAppSettings");
builder.Services.Configure<DroppshippingAppSettings>(dropShipSettings);
DroppshippingAppSettings dropShipAppSettings = dropShipSettings.Get<DroppshippingAppSettings>();

// Configure Swagger/OpenAPI
builder.Services.ConfigureSwagger();

// Register the database context
builder.Services.AddDbContext<DropShippingDbContext>(options =>
    options.UseSqlServer(dropShipAppSettings.ConnectionString));

// Dependency Injection for repositories and services
DependencyInjectionHelper.InjectRepositories(builder.Services);
DependencyInjectionHelper.InjectServices(builder.Services);

// Configure JWT Authentication
builder.Services.ConfigureAuthentication(dropShipAppSettings.SecretKey);

// CORS Policy
builder.Services.ConfigureCORSPolicy();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting(); // Add UseRouting before UseCors

app.UseCors("AllowAll"); // Place UseCors after UseRouting

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Run the application
app.Run();

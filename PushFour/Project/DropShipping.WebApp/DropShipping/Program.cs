using DropShipping.DataBase.Context;
using DropShipping.Helpers;
using DropShipping.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load configuration for DropShippingAppSettings
//var dropShipSettings = builder.Configuration.GetSection("DropShippingAppSettings");
//builder.Services.Configure<DroppshippingAppSettings>(dropShipSettings);
var dropShipSettings = builder.Configuration.GetSection("DropShippingAppSettings");
builder.Services.Configure<DroppshippingAppSettings>(dropShipSettings);
DroppshippingAppSettings dropShipAppSettings = dropShipSettings.Get<DroppshippingAppSettings>();

var dropShipAppSettingss = dropShipSettings.Get<DroppshippingAppSettings>();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Loggers
// Uncomment if using Serilog
// builder.Host.UseSerilog();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Run the application
app.Run();

using DropShipping.DataBase.Context;
using DropShipping.Shared.Configuration;
using DropShipping.Helpers;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dropShipSettings = builder.Configuration.GetSection("DroppshippingAppSettings");
builder.Services.Configure<DroppshippingAppSettings>(dropShipSettings);
DroppshippingAppSettings dropShipAppSettings = dropShipSettings.Get<DroppshippingAppSettings>();

// loggers

//builder.Host.UseSerilog(); / uncomment if using loggers
//

// REGISTER DATABASE
builder.Services.AddDbContext<DropShippingDbContext>(options => options.UseSqlServer(dropShipAppSettings.ConnectionString));
//

// DP INJECTION
builder.Services.InjectRepositories();
builder.Services.InjectServices();
//

// Configure JWT
builder.Services.ConfigureAuthentication(dropShipAppSettings.SecretKey);
//

// CORS POLICY
builder.Services.ConfigureCORSPolicy();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// allowing every request BE to FE
app.UseCors("AllowAll");
//

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

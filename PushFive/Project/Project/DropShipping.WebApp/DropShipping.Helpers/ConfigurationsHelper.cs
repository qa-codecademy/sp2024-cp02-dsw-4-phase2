using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System.Text;

namespace DropShipping.Helpers
{
    public static class ConfigurationsHelper
    {
        public static void ConfigureAuthentication(this IServiceCollection services, string secretKey)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey))
                    };
                });
        }

        public static void ConfigureCORSPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
        }

        public static ILogger GetSerilogConfiguration(string connectionString)
        {
            return new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                    .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
                    //.WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] : {Message}{NewLine:1}{Exception:1}")
                    //.WriteTo.File(path: "./Logs/noteAppLogs.txt", outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] : {Message}{NewLine:1}{Exception:1}")
                    .WriteTo.MSSqlServer(
                        connectionString: connectionString,
                        sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = "Logs",
                            SchemaName = "dbo",
                            AutoCreateSqlTable = true,
                        },
                        restrictedToMinimumLevel: LogEventLevel.Information
                    )
                    .CreateLogger();
        }
    }
}
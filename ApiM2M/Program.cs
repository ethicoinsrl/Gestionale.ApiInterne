
using Amazon.CloudWatchLogs;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using ApiM2M.Business.Jointly.Business;
using ApiM2M.Business.Jointly.Interfaces;
using ApiM2M.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.AwsCloudWatch;

namespace TestApiM2M
{
    public class Program
    {
        public static IConfiguration Configuration { get; private set; }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Configuration = builder.Configuration;

            builder.Services.AddDbContext<HealthHubContext>(o =>
                o.UseSqlServer(builder.Configuration.GetConnectionString("HealthHubDB"),
                x =>
                {
                    x.UseNetTopologySuite();
                    x.CommandTimeout(120);
                })
            );

            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = builder.Configuration["Jwt:Authority"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidateAudience = false,
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ValidateLifetime = true,
                    };
                });
            
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireJointlyScope", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var scopeClaim = context.User.FindFirst("scope");
                        if (scopeClaim == null) return false;
                        var scopes = scopeClaim.Value.Split(' ');
                        return scopes.Any(s => s.EndsWith("jointly"));
                    });
                });
            });

            #region SERILOG
            var _environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var operationsLogGroup = $"LOGOPERAZIONI/{_environmentName}/HealthHubMVC";

            if (!_environmentName.ToUpper().Contains("DEVELOPMENT"))// Aggiungi || true per testare il logging su CloudWatch
            {
                var store = new CredentialProfileStoreChain();
                AmazonCloudWatchLogsClient client;
                if (store.TryGetAWSCredentials("HealthHub", out AWSCredentials _creds))
                    client = new AmazonCloudWatchLogsClient(_creds, Amazon.RegionEndpoint.EUCentral1);
                else
                    client = new AmazonCloudWatchLogsClient(Amazon.RegionEndpoint.EUCentral1);

                // Configurazione per i log applicativi
                var appLogOptions = new CloudWatchSinkOptions
                {
                    LogGroupName = $"HealthHub/{_environmentName}",
                    TextFormatter = new JsonFormatter(),
                    MinimumLogEventLevel = LogEventLevel.Debug,
                    BatchSizeLimit = 100,
                    QueueSizeLimit = 10000,
                    Period = TimeSpan.FromSeconds(10),
                    CreateLogGroup = true,
                    LogStreamNameProvider = new DefaultLogStreamProvider(),
                    RetryAttempts = 3
                };

                // Configurazione per i log delle operazioni
                var operationsLogOptions = new CloudWatchSinkOptions
                {
                    LogGroupName = operationsLogGroup,
                    TextFormatter = new JsonFormatter(),
                    MinimumLogEventLevel = LogEventLevel.Information,
                    BatchSizeLimit = 100,
                    QueueSizeLimit = 10000,
                    Period = TimeSpan.FromSeconds(10),
                    CreateLogGroup = true,
                    LogStreamNameProvider = new DefaultLogStreamProvider(),
                    RetryAttempts = 3
                };

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.AspNetCore.Localization", LogEventLevel.Error)
                    .Enrich.WithProperty("ProjectName", "HealthHubMVC")
                    .Enrich.WithEnvironmentName()
                    .Enrich.WithMachineName()
                    .Enrich.FromLogContext()
                    .WriteTo.Logger(lc => lc
                        .WriteTo.AmazonCloudWatch(appLogOptions, client)
                        .Filter.ByExcluding(e => e.Properties.ContainsKey("Source") &&
                             e.Properties["Source"].ToString().Contains("LOG_OPERATION")))
                    .WriteTo.Logger(lc => lc
                         .WriteTo.AmazonCloudWatch(operationsLogOptions, client)
                         .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Source") &&
                             e.Properties["Source"].ToString().Contains("LOG_OPERATION")))
                    .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.AspNetCore.Localization", LogEventLevel.Error)
                    .Enrich.WithProperty("ProjectName", "HealthHubMVC")
                    .Enrich.WithEnvironmentName()
                    .Enrich.WithMachineName()
                    .Enrich.FromLogContext()
                    .WriteTo.Console(
                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message}{NewLine}{Exception}")
                    .CreateLogger();

                Log.Debug("Serilog configuration completed for Development environment");
            }
            builder.Host.UseSerilog();
            #endregion

            builder.Services.AddScoped<IJointlyBusiness, JointlyBusiness>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

using System.Text.Json;
using System.Text.Json.Serialization;
using NLog;
using Todo.Extentions;
using Todo.Shared.Utils;
using ILogger = NLog.ILogger;

namespace Todo;

public class Startup
{
    private static Logger _logger = LogManager.GetCurrentClassLogger();
    public IConfiguration Configuration { get; }
    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services for controllers
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
        
        // Adds health check service
        services.AddHealthChecks();
        
        services.AddSingleton<ILogger, Logger>(x => _logger); // Add a singleton service of the ILogger type to the services container.
        services.ConfigureScopeFacades();
        services.ConfigureScopeRepository();
        services.ConfigureScopeService();
        services.ConfigureScopeMappers();
        services.ConfigureAddHttpClient();
    }
    
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDefaultException(_logger, true); // Use the default exception handler.
        }
        else
        {
            app.UseDefaultException(_logger, false);
        }
        
        app.UseRouting();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health");
        });
    }
}
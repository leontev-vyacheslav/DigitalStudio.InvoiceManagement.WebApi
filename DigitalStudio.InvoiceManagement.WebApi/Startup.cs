using DigitalStudio.InvoiceManagement.WebApi.Models.Configs;
using DigitalStudio.InvoiceManagement.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace DigitalStudio.InvoiceManagement.WebApi;

public class Startup
{
    public IConfiguration Configuration { get; }

    public readonly ApplicationConfig ApplicationConfig = new();

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        configuration.Bind(ApplicationConfig);
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddSingleton<IApplicationConfig>(ApplicationConfig)
            .AddCors()
            .AddDbContext<AppDataContext>(options => options.UseInMemoryDatabase("AppDatabase"))
            .AddAutoMapper(typeof(MapperProfile))
            .AddSingleton<PersistentStorageService>()
            .AddHostedService<AppDataContextHostedService>()
            .AddAppCommands()
            .AddControllers()
            .AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "DigitalStudio.InvoiceManagement.WebApi"
            });
            options.EnableAnnotations();
        });
    }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseStaticFiles()
                .UseSwagger(options => { options.RouteTemplate = "docs/api/{documentname}/schema.json"; })
                .UseSwaggerUI(options =>
                {
                    options.RoutePrefix = "docs/api";
                    options.DocumentTitle = "DigitalStudio InvoiceManagement API Console";
                    options.InjectStylesheet("/content/css/swagger-custom.css");
                    options.InjectJavascript("/content/js/swagger-custom.js");
                    options.SwaggerEndpoint("/docs/api/v1/schema.json", "DigitalStudio.InvoiceManagement.WebApi v1");
                });

            app.UseRouting();
            app.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            app.UseEndpoints(options => { options.MapControllers(); });
        }
    }
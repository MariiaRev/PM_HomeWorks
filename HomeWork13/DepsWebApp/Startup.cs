using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using DepsWebApp.Clients;
using DepsWebApp.Extensions;
using DepsWebApp.Options;
using DepsWebApp.Services;
using DepsWebApp.Authentication;
using DepsWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DepsWebApp
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add options
            services
                .Configure<CacheOptions>(Configuration.GetSection("Cache"))
                .Configure<NbuClientOptions>(Configuration.GetSection("Client"))
                .Configure<RatesOptions>(Configuration.GetSection("Rates"));

            // Add authentication
            services
                .AddAuthentication(CustomAuthScheme.Name)
                .AddScheme<CustomAuthSchemeOptions, CustomAuthSchemeHandler>(
                    CustomAuthScheme.Name, CustomAuthScheme.Name, null);

            // Add application services
            services.AddScoped<IRatesService, RatesService>();
            services.AddScoped<IAuthService, AuthPostgresService>();

            // Add NbuClient as Transient
            services.AddHttpClient<IRatesProviderClient, NbuClient>()
                .ConfigureHttpClient(client => client.Timeout = TimeSpan.FromSeconds(10));

            // Add CacheHostedService as Singleton
            services.AddHostedService<CacheHostedService>();

            // Add batch of Swashbuckle Swagger services
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DI Demo App API", Version = "v1" });
                var filePath = Path.Combine(AppContext.BaseDirectory, "DepsWebApp.xml");
                c.IncludeXmlComments(filePath);
                c.EnableAnnotations();

                // enable swagger for authorization
                c.AddSecurityRequirement(
                        new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Id = "Session",
                                        Type = ReferenceType.SecurityScheme
                                    },
                                },
                                new string[0]
                            }
                        });

                c.AddSecurityDefinition(
                    "Session",
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.ApiKey,
                        In = ParameterLocation.Header,
                        Scheme = "Session",
                        Name = "Authorization",
                        Description = "SessionId",
                        BearerFormat = "SessionId"
                    });
            });

            // Add batch of framework services
            services.AddMemoryCache();
            services.AddControllers();

            // Add db context
            services.AddDbContext<DepsWebAppContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DI Demo App API v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCustomLogging();                         //connect logging middleware

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // add migrations
            using var scope = app.ApplicationServices.CreateScope();
            await using var depsWebAppContext = scope.ServiceProvider.GetRequiredService<DepsWebAppContext>();
            await depsWebAppContext.Database.MigrateAsync();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

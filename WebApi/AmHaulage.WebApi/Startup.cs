// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmHaulage.WebApi
{
    using System.Diagnostics.CodeAnalysis;
    using AmHaulage.Persistence;
    using AmHaulage.Persistence.Contracts;
    using AmHaulage.Services;
    using AmHaulage.Services.Contracts;
    using AmHaulage.Services.Contracts.Mappers;
    using AmHaulage.Services.Contracts.Validators;
    using AmHaulage.Services.Mappers;
    using AmHaulage.Services.Validators;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// Startup is responsible for initializing services that will be used
    /// in the ASP.NET Core application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRepositoryFactory, RepositoryFactory>();
            services.AddSingleton<ICalendarEventMapper, CalendarEventMapper>();
            services.AddSingleton<ICalendarEventValidator, CalendarEventValidator>();
            services.AddSingleton<IEventCreatorService, EventCreatorService>();
            services.AddSingleton<IEventDeleterService, EventDeleterService>();
            services.AddSingleton<IEventReaderService, EventReaderService>();
            services.AddSingleton<IEventUpdaterService, EventUpdaterService>();
            services.AddSingleton<IMonthReaderService, MonthReaderService>();

            // Register services required to support API versioning
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "AllowAll",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddMvcCore();
            services.AddApiVersioning(
                o =>
                {
                    o.Conventions.Add(new VersionByNamespaceConvention());
                });

            services.AddControllers();

            // Register services to support Swagger
            services.AddVersionedApiExplorer(
                o =>
                {
                    o.GroupNameFormat = "VVVV";
                    o.SubstituteApiVersionInUrl = true;
                    o.SubstitutionFormat = "VVVV";
                });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The web host environment.</param>
        /// <param name="provider">The API version description provider.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(
                    options =>
                    {
                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            options.SwaggerEndpoint(
                                $"/swagger/{description.GroupName}/swagger.json",
                                description.GroupName.ToUpperInvariant());
                        }
                    });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors("AllowAll");
            });
        }
    }
}

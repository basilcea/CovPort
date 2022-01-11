using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.IO;
using System;
using FluentValidation.AspNetCore;
using Api.Validations;
using Api.Mappings;
using Application;
using Infrastructure;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Api.HealthChecks;
using Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ParentDirectory = Directory.GetParent(Directory.GetCurrentDirectory());
        }

        public IConfiguration Configuration { get; }
        public DirectoryInfo ParentDirectory { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(options =>
                {
                    options.Filters.Add<ValidationFilterAttribute>();
                    options.Filters.Add<UnhandledExceptionFilterAttribute>();
                }
            )
            .AddNewtonsoftJson()
            .AddFluentValidation(cfg =>
                    cfg.RegisterValidatorsFromAssemblyContaining<BookingPostRequestValidator>()
                );
            // In production, the React files will be served from this directory
            // services.AddSpaStaticFiles(configuration =>
            // {
            //     configuration.RootPath = $"{Directory.GetParent(ParentDirectory.FullName)}/Client/build";
            // });
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
            services.AddAutoMapper(cfg => cfg.AddProfile<RequestToCommandProfile>());
            services.AddHealthChecks().AddCheck<DbHealthCheck>("Database");

            services.AddInfrastructure(Configuration);
            services.AddApplication();
    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void  Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "Api v1"));
            // app.UseSerilogRequestLogging();

            if (!env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }
    
            app.UseStaticFiles();
            // app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapGet("/", async context =>
                {
                    var hostUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}";
                    var info = new
                    {
                        name = "CovPort",
                        version = "1.0.0",
                        health = $"{hostUrl}/health",
                        documentation = $"{hostUrl}/swagger"
                    };
                    var infoJson = JsonConvert.SerializeObject(info);
                    await context.Response.WriteAsync(infoJson);
                });
            });

            // app.UseSpa(spa =>
            // {
            //     spa.Options.SourcePath = $"{Directory.GetParent(ParentDirectory.FullName)}/Client";

            //     if (env.IsDevelopment())
            //     {
            //         // spa.UseReactDevelopmentServer(npmScript: "start");
            //         spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
            //     }
            // });
        }
    }
}

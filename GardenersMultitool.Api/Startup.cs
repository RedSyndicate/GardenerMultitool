using System;
using System.Collections.Concurrent;
using GardenersMultitool.Api.CustomConfigurations;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.ValueObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GardenersMultitool.Api
{
    public class Startup
    {
        private readonly string _contentRoot;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _contentRoot = configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));
            services.AddSingleton<PlantService>();

            services
                .AddPlantCache(_contentRoot)
                .AddLocationCache()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GardenersMultitool.Api", Version = "v1" });
                });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(options =>
                {
                    options.AllowAnyOrigin();
                });
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GardenersMultitool.Api v1"));
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

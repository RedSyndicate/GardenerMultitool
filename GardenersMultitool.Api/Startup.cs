using System;
using System.Reflection;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace GardenersMultitool.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly)
            .Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"))
            .AddSingleton(config =>
            {
                var settings = config.GetService<IOptions<DatabaseSettings>>()?.Value;

                if (settings != null)
                    return new MongoClient(settings.ConnectionString).GetDatabase(settings.Database);
                throw new MongoConfigurationException($"Uninitialized Database Settings");
            })
            .AddSingleton<ICollectionProxy<Plant>, PlantCollection>()
            .AddSingleton<ICollectionProxy<Location>, LocationCollection>()
            .AddSingleton<ICollectionProxy<ZipcodeHardinessZone>, ZipcodeHardinessCollection>()
            .AddSingleton<DataContext>()
            .AddSwaggerGen(c =>
            {
                c.UseOneOfForPolymorphism();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GardenersMultitool.Api", Version = "v1" });
            })
            .AddResponseCaching();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Docker"))
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(options =>
                {
                    options.AllowAnyOrigin();
                });
            }
            app.UseResponseCaching();

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

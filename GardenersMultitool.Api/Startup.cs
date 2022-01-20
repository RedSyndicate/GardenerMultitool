using System.Reflection;
using CsvHelper.Configuration;
using GardenersMultitool.Api.UseCases.Context;
using GardenersMultitool.Domain.Entities;
using GardenersMultitool.Domain.ValueObjects;
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
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly)
            .Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"))
            .AddSingleton(config =>
            {
                var settings = config.GetService<IOptions<DatabaseSettings>>()?.Value;
                if (settings != null) return new MongoClient(settings.ConnectionString).GetDatabase(settings.Database);
                throw new ConfigurationException($"Uninitialized Database Settings");
            })
            .AddSingleton<ICollectionProxy<Plant>, PlantCollection>()
            .AddSingleton<ICollectionProxy<Location>, LocationCollection>()
            .AddSingleton<DataContext>()
            .AddSingleton<PlantService>()
                //.AddPlantCache(_contentRoot) // Load files from api project directory
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

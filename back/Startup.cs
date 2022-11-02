using System;
using System.IO;
using System.Reflection;
using dot.Models;
using dot.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace dot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Console.WriteLine("hello2");
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("hello3");
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSingleton<IotService>();
            services.AddSingleton<SensorService>();
            services.Configure<IotDatabaseSettings>(Configuration.GetSection("IotDatabase"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            Console.WriteLine("hello4");
            //if (app.env.IsDevelopment())
            {
                Console.WriteLine("hello5");
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
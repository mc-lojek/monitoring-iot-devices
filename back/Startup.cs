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
            Console.Write("hello2");
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            Console.Write("hello3");
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSingleton<IotService>();
            services.Configure<IotDatabaseSettings>(Configuration.GetSection("IotDatabase"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.Write("hello4");
            if (env.IsDevelopment())
            {
                Console.Write("hello5");
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
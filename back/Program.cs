using System;
using dot.Models;
using dot.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace dot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("hello");
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(builder => { builder.UseStartup<Startup>(); }).Build().Run();
            Console.WriteLine("123hello");

        }
    }
}
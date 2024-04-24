using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using HomeRunIQ.gRPCServer;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Configure the web host
var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    });

hostBuilder.Build().Run();

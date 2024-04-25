using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HomeRunIQ.gRPCServer.Services;
using Grpc.AspNetCore.Web;
using Microsoft.Extensions.Configuration;

namespace HomeRunIQ.gRPCServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddHttpClient();
            services.AddSingleton<ContentService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin() // Be more specific in production!
                                 .AllowAnyMethod()
                                 .AllowAnyHeader()
                                 .WithExposedHeaders("grpc-status", "grpc-message", "grpc-status-details-bin");
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>().RequireCors("AllowAll").EnableGrpcWeb();
                endpoints.MapGrpcService<ContentService>().RequireCors("AllowAll").EnableGrpcWeb();
            });
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HomeRunIQ.gRPCServer.Services;
using Grpc.AspNetCore.Web;

namespace HomeRunIQ.gRPCServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();

            // Configure CORS to allow specific or all origins
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin()
                                 .AllowAnyMethod()
                                 .AllowAnyHeader();
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

            // Apply CORS globally
            app.UseCors("AllowAll");

            // Apply gRPC-Web middleware globally to support calling gRPC methods from web browsers
            app.UseGrpcWeb();

            // Map gRPC services with CORS and gRPC-Web enabled
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>().RequireCors("AllowAll").EnableGrpcWeb();
                // Additional gRPC services can be mapped here
            });
        }
    }
}

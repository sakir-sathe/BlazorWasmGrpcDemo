using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using HomeRunIQ.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HomeRunIQ.Proto; // Ensure this using directive is added to access proto files

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Standard HTTP client
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


//In real time project let it come from appsettings
var backendUrl = "https://localhost:7055";  // This should be the same backend URL if hosting on the same server
var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
var channel = GrpcChannel.ForAddress(backendUrl, new GrpcChannelOptions { HttpHandler = httpHandler });

// Configure GreeterClient 
builder.Services.AddSingleton(services =>
{

    var channel = GrpcChannel.ForAddress(backendUrl, new GrpcChannelOptions { HttpHandler = httpHandler });
    return new Greeter.GreeterClient(channel);
});

// Configure the ContentServiceClient for gRPC
builder.Services.AddSingleton(services =>
{
    return new ContentService.ContentServiceClient(channel);
});


await builder.Build().RunAsync();

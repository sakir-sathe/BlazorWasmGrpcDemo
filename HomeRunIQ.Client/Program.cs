using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using HomeRunIQ.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HomeRunIQ.Proto; // Ensure this using directive is added to access GreeterClient

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Standard HTTP client
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Configure GrpcClient
builder.Services.AddSingleton(services => {
    var backendUrl = "https://localhost:7055"; // Adjust this as needed for your environment
    var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());

    var channel = GrpcChannel.ForAddress(backendUrl, new GrpcChannelOptions { HttpHandler = httpHandler });
    return new Greeter.GreeterClient(channel); // Correctly creating and registering the GreeterClient
});

await builder.Build().RunAsync();

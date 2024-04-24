# BlazorWasmGrpcDemo
BlazorWasmGrpcDemo is a fully-functional demo project showcasing the integration of Blazor WebAssembly with a backend ASP.NET gRPC service. It's designed as a practical starter for developers interested in learning about Blazor and gRPC in .NET 8.

## Features
- **Blazor WebAssembly Client**: A standalone frontend application that communicates with the backend using gRPC.
- **ASP.NET Core gRPC Service**: Backend service that handles gRPC calls and communicates with the Blazor frontend.
- **Protobuf Definitions**: Includes `.proto` files defining the service and messages, facilitating type-safe communication.

## Prerequisites
Before running this project, ensure you have the following installed:
- .NET 8 Runtime
- An appropriate IDE (like Visual Studio 2022) to run .NET applications

## Getting Started
1. **Clone the Repository**

2. **Open the Solution**
Open the solution file in Visual Studio or your preferred IDE.

3. **Set Multiple Startup Projects**
Configure the solution to start both the Blazor frontend and the ASP.NET Core gRPC service simultaneously.

4. **Run the Solution**
Build and run the solution. The Blazor app should open in your browser, and you can interact with the gRPC backend through the UI.

## Contributing
Contributions are welcome! Please feel free to submit pull requests, suggest features, or report bugs.

## License
[MIT](LICENSE) - Feel free to use this project for any purpose, from education to personal or commercial projects.

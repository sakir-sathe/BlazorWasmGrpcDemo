using Grpc.Core;
using HomeRunIQ.Proto;

namespace HomeRunIQ.gRPCServer.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = $"Hello {request.Name}, welcome! This response is coming from the backend gRPC service, which is currently up and running."
            });
        }

    }
}

using Grpc.Core;
using HomeRunIQ.Proto;
using HomeRunIQ.Shared.Model;
using Newtonsoft.Json;
using System.Text.Json;

namespace HomeRunIQ.gRPCServer.Services
{
    public class ContentService : Proto.ContentService.ContentServiceBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ContentService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ApiSettings:ApiKey"]; // Generate your own key and add it in appsettings

        }

        public override async Task<SearchGifsResponse> SearchGifs(SearchGifsRequest request, ServerCallContext context)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://tenor.googleapis.com/v2/search?q={request.Query}&key={_apiKey}&limit=10");

                if (!response.IsSuccessStatusCode)
                {
                    throw new RpcException(new Status(StatusCode.Internal, $"Failed to retrieve GIFs from Tenor: {response.ReasonPhrase}"));
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var tenorResponse = JsonConvert.DeserializeObject<TenorApiResponse>(jsonString);
                if (tenorResponse == null)
                {
                    throw new RpcException(new Status(StatusCode.Internal, "Invalid JSON format received from Tenor API."));
                }

                var searchResults = new SearchGifsResponse();
                foreach (var result in tenorResponse.Results)
                {
                    if (result.Media_Formats?.Gif != null)
                    {
                        searchResults.Results.Add(new GifResult
                        {
                            Url = result.Media_Formats.Gif.Url,
                            Title = result.Title
                        });
                    }
                }
                return searchResults;
            }
            catch (Exception e)
            {
                // Log the exception details for debugging purposes
                Console.WriteLine($"Unexpected exception: {e.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "An unexpected error occurred."));
            }
        }

    }
}

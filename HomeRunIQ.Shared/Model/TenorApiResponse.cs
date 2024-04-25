
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomeRunIQ.Shared.Model
{
    public class TenorApiResponse
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }
    }

    public class Result
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("media_formats")]
        public MediaFormats Media_Formats { get; set; }
    }

    public class MediaFormats
    {
        [JsonProperty("gif")]
        public MediaFormat Gif { get; set; }
    }

    public class MediaFormat
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace TestApp.Models
{
    public class BeerDetail
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("brandName")]
        public string BrandName { get; init; } = default!;

        [JsonPropertyName("name")]
        public string Name { get; init; } = default!;

        [JsonPropertyName("articles")]
        public BeerArticleDetail[] Articles { get; init; } = default!;
    }
}

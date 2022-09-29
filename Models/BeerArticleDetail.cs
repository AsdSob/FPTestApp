using System.Text.Json.Serialization;

namespace TestApp.Models
{
    public class BeerArticleDetail
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("shortDescription")]
        public string Description { get; init; } = default!;

        [JsonPropertyName("price")]
        public float Price { get; init; }

        [JsonPropertyName("image")]
        public string Image { get; init; } = default!;

        [JsonPropertyName("unit")]
        public string Unit { get; init; } = default!;

        [JsonPropertyName("pricePerUnitText")]
        public string PricePerUnit { get; init; } = default!;
    }
}

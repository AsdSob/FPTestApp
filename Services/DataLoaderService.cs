using System.Text.Json;
using TestApp.Models;

namespace TestApp.Services
{
    public class DataLoaderService : IDataLoaderService
    {
        private readonly HttpClient httpClient;

        public DataLoaderService(
                    HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<BeerDetail[]> LoadAsync(string url, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = await httpClient.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
                var items = await JsonSerializer.DeserializeAsync<BeerDetail[]>(stream, cancellationToken: cancellationToken);
                return items ?? Array.Empty<BeerDetail>();
            }
            else
                throw new Exception("Can't load image");

        }
    }
}

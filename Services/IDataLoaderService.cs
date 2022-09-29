using TestApp.Models;

namespace TestApp.Services
{
    public interface IDataLoaderService
    {
        Task<BeerDetail[]> LoadAsync(string url, CancellationToken cancellationToken);
    }
}

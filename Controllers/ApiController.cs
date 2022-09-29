using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TestApp.App.Extensions;
using TestApp.Models;
using TestApp.Services;

namespace TestApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> logger;
        private readonly IDataLoaderService dataLoaderService;

        public ApiController(
                    ILogger<ApiController> logger,
                    IDataLoaderService dataLoaderService)
        {
            this.logger = logger;
            this.dataLoaderService = dataLoaderService;
        }

        [HttpGet("MostExpensiveAndCheapest")]
        public async Task<ExpensiveCheapestDetail> MostExpensiveAndCheapestAsync([FromQuery][Required] string url, CancellationToken cancellationToken)
        {
            logger.LogTrace($"{nameof(MostExpensiveAndCheapestAsync)} executed");

            var items = await dataLoaderService.LoadAsync(url, cancellationToken);
            return items.MostExpensiveAndCheapest();
        }

        [HttpGet("CostExactly")]
        public async Task<BeerDetail[]> CostExactlyAsync([FromQuery][Required] string url, [FromQuery][Required] float price, CancellationToken cancellationToken)
        {
            logger.LogTrace($"{nameof(CostExactlyAsync)} executed");

            var items = await dataLoaderService.LoadAsync(url, cancellationToken);
            return items.CostExactly(price);
        }

        [HttpGet("MostBottles")]
        public async Task<BeerDetail> MostBottlesAsync([FromQuery][Required] string url, CancellationToken cancellationToken)
        {
            logger.LogTrace($"{nameof(MostBottlesAsync)} executed");

            var items = await dataLoaderService.LoadAsync(url, cancellationToken);
            return items.MostBottles();
        }

        [HttpGet("All")]
        public async Task<AllDto> AllAsync([FromQuery][Required] string url, [FromQuery][Required] float price, CancellationToken cancellationToken)
        {
            logger.LogTrace($"{nameof(AllAsync)} executed");

            var items = await dataLoaderService.LoadAsync(url, cancellationToken);
            return new AllDto
            {
                MostExpensiveAndCheapest = items.MostExpensiveAndCheapest(),
                CostExactly = items.CostExactly(price),
                MostBottles = items.MostBottles()
            };
        }
    }
}
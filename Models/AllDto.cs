namespace TestApp.Models
{
    public class AllDto
    {
        public ExpensiveCheapestDetail MostExpensiveAndCheapest { get; init; } = default!;

        public BeerDetail[] CostExactly { get; init; } = default!;

        public BeerDetail MostBottles { get; init; } = default!;
    }
}

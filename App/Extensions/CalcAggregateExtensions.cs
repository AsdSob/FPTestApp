using System.Globalization;
using TestApp.Models;

namespace TestApp.App.Extensions
{
    public static class CalcAggregateExtensions
    {
        public static ExpensiveCheapestDetail MostExpensiveAndCheapest(this BeerDetail[] items)
        {
            var sortedItems = items.SelectMany(x => x.Articles
                                                     .Select(f => Tuple.Create(ParsePricePerUnit(f.PricePerUnit), x, f)))
                                   .OrderBy(x => x.Item1)
                                   .ToArray();

            var cheapest = sortedItems[0];
            var cheapestBeer = cheapest.Item2;
            var expensive = sortedItems[^1];
            var expensiveBeer = expensive.Item2;

            return new ExpensiveCheapestDetail()
            {
                Cheapest = new BeerDetail
                {
                    Id = cheapestBeer.Id,
                    Name = cheapestBeer.Name,
                    BrandName = cheapestBeer.BrandName,
                    Articles = new[] { cheapest.Item3 }
                },
                Expensive = new BeerDetail
                {
                    Id = expensiveBeer.Id,
                    Name = expensiveBeer.Name,
                    BrandName = expensiveBeer.BrandName,
                    Articles = new[] { expensive.Item3 }
                }
            };
        }

        public static BeerDetail[] CostExactly(this BeerDetail[] items, float price)
        {
            return items.SelectMany(x => x.Articles
                                          .Where(f => f.Price == price)
                                          .Select(f => Tuple.Create(ParsePricePerUnit(f.PricePerUnit), x, f)))
                        .OrderBy(x => x.Item1)
                        .Select(x => {
                            var beer = new BeerDetail
                            {
                                Id = x.Item2.Id,
                                Name = x.Item2.Name,
                                BrandName = x.Item2.BrandName,
                                Articles = new[] { x.Item3 }
                            };
                            return beer;
                        })
                        .ToArray();
        }

        public static BeerDetail MostBottles(this BeerDetail[] items)
        {
            var mostItem = items.SelectMany(x => x.Articles
                                                   .Select(f => Tuple.Create(ParseBottleCount(f.Description), x, f)))
                                .OrderByDescending(x => x.Item1)
                                .First();
            return new BeerDetail
            {
                Id = mostItem.Item2.Id,
                Name = mostItem.Item2.Name,
                BrandName = mostItem.Item2.BrandName,
                Articles = new[] { mostItem.Item3 }
            };
        }

        private static float ParsePricePerUnit(string text)
        {
            text = text.Trim('(', ')');
            var parts = text.Split('/')[0].Split(' ');
            return float.Parse(parts[0].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture);
        }

        private static int ParseBottleCount(string text)
        {
            text = text.Split('x')[0].Trim();
            return int.Parse(text);
        }
    }
}

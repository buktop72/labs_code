using System.Linq;
using TvStore.Models;

namespace TvStore
{
    public static class SampleData
    {
        public static void Initialize(TvContext context)
        {
            if (!context.Tv.Any())
            {
                context.Tv.AddRange(
                    new Tv
                    {
                        Name = "Philips 43PUS7956",
                        Company = "Philips",
                        Size = 55,
                        Price = 550
                    },
                    new Tv
                    {
                        Name = "Samsung UE32T4500",
                        Company = "Samsung",
                        Size = 32,
                        Price = 390
                    },
                    new Tv
                    {
                        Name = "Telefunken TF-LED24S80T2",
                        Company = "Telefunken",
                        Size = 24,
                        Price = 249
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
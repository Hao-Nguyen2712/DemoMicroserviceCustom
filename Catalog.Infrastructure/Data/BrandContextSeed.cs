using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public class BrandContextSeed
    {
        public static async Task SeedData(IMongoCollection<ProductBrand> brandCollection)
        {
            bool checkBrand = brandCollection.Find(p => true).Any();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Data", "SeedData", "brands.json");
            if (!checkBrand)
            {
                var brandData = File.ReadAllText(path);
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                if (brands != null)
                {
                    foreach (var item in brands)
                    {
                        await brandCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}

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
   public class TypeContextSeed
    {
        public static void SeedData(IMongoCollection<ProductType> typedCollection)
        {
            bool checkType = typedCollection.Find(p => true).Any();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Data", "SeedData", "types.json");
            if (!checkType)
            {
                var typeData = File.ReadAllText(path);
                var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                if (types != null)
                {
                    foreach (var item in types)
                    {
                        typedCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}

using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
   public interface ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<ProductType> Types { get; }
        public IMongoCollection<ProductBrand> Brands { get; }
    }
}

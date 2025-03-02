using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using Catalog.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypeRepository
    {
        private readonly ICatalogContext catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            this.catalogContext = catalogContext;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await catalogContext.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await catalogContext.Products.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await catalogContext.Brands.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await catalogContext.Types.Find(p => true).ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Pagination<Product>> GetProductByName(string name, CatalogSpecParams catalogSpecParams)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

            if (!string.IsNullOrEmpty(catalogSpecParams.Sort)) {
                return new Pagination<Product>
                {
                    PageSize = catalogSpecParams.PageSize,
                    PageIndex = catalogSpecParams.PageIndex,
                    Data = await DataFilter(catalogSpecParams, filter),
                    Count = (int)await catalogContext.Products.CountDocumentsAsync(p => true) //TODO: Need to check while applying with UI
                };
            }

            return new Pagination<Product>
            {
                PageSize = catalogSpecParams.PageSize,
                PageIndex = catalogSpecParams.PageIndex,
                Data = await catalogContext
                    .Products
                    .Find(filter)
                    .Sort(Builders<Product>.Sort.Ascending("Name"))
                    .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                    .Limit(catalogSpecParams.PageSize)
                    .ToListAsync(),
                Count = (int)await catalogContext.Products.CountDocumentsAsync(p => true)
            };

        }

        public async Task<Pagination<Product>> GetProducts(CatalogSpecParams specParams)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(specParams.Search))
            {
                var searhcFilter = builder.Regex(x => x.Name, new BsonRegularExpression(specParams.Search));
                filter &= searhcFilter;
            }
            if (!string.IsNullOrEmpty(specParams.BrandId))
            {
                var brandFilter = builder.Regex(x => x.Brands.Id, new BsonRegularExpression(specParams.BrandId));
                filter &= brandFilter;
            }
            if (!string.IsNullOrEmpty(specParams.TypesId))
            {
                var typeFilter = builder.Regex(x => x.Type.Id, new BsonRegularExpression(specParams.TypesId));
                filter &= typeFilter;
            }
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                return new Pagination<Product>
                {
                    PageSize = specParams.PageSize,
                    PageIndex = specParams.PageIndex,
                    Data = await DataFilter(specParams, filter),
                    Count = (int)await catalogContext.Products.CountDocumentsAsync(p => true) //TODO: Need to check while applying with UI
                };
            }
            return new Pagination<Product>
            {
                PageSize = specParams.PageSize,
                PageIndex = specParams.PageIndex,
                Data = await catalogContext
                  .Products
                  .Find(filter)
                  .Sort(Builders<Product>.Sort.Ascending("Name"))
                  .Skip(specParams.PageSize * (specParams.PageIndex - 1))
                  .Limit(specParams.PageSize)
                  .ToListAsync(),
                Count = (int)await catalogContext.Products.CountDocumentsAsync(p => true)
            };
        }

        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
        {
            switch (catalogSpecParams.Sort)
            {
                case "priceAsc":
                    return await catalogContext
                        .Products
                        .Find(filter)
                        .Sort(Builders<Product>.Sort.Ascending("Price"))
                        .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                        .Limit(catalogSpecParams.PageSize)
                        .ToListAsync();
                case "priceDesc":
                    return await catalogContext
                        .Products
                        .Find(filter)
                        .Sort(Builders<Product>.Sort.Descending("Price"))
                        .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                        .Limit(catalogSpecParams.PageSize)
                        .ToListAsync();
                default:
                    return await catalogContext
                        .Products
                        .Find(filter)
                        .Sort(Builders<Product>.Sort.Ascending("Name"))
                        .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                        .Limit(catalogSpecParams.PageSize)
                        .ToListAsync();
            }
        }


        public async Task<IEnumerable<Product>> GetProductsByBrand(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Brands.Name, name);
            return await catalogContext.Products.Find(filter).ToListAsync();
        }


        public async Task<IEnumerable<Product>> GetProductsByType(string type)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Type.Name, type);
            return await catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await catalogContext.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}

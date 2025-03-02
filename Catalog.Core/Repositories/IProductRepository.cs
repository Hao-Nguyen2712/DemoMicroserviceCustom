using Catalog.Core.Entities;
using Catalog.Core.Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Pagination<Product>> GetProducts(CatalogSpecParams specParams);
        Task<Product> GetProduct(string id);
        Task<Pagination<Product>> GetProductByName(string name, CatalogSpecParams catalogSpecParams);
        Task<IEnumerable<Product>> GetProductsByType(string type);
        Task<IEnumerable<Product>> GetProductsByBrand(string name);
        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}

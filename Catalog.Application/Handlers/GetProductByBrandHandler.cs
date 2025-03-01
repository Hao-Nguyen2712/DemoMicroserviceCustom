using Catalog.Application.Mappers;
using Catalog.Application.Querries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetProductByBrandHandler : IRequestHandler<GetProductByBrandQuerry, IList<ProductResponse>>
    {
        private IProductRepository productRepository;

        public GetProductByBrandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<IList<ProductResponse>> Handle(GetProductByBrandQuerry request, CancellationToken cancellationToken)
        {
            var productList = await productRepository.GetProductsByBrand(request.BrandName);
            var productResponse = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);
            return productResponse;
        }
    }
}

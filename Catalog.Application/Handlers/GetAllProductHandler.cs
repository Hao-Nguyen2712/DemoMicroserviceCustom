using Catalog.Application.Mappers;
using Catalog.Application.Querries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetAllProductHandler : IRequestHandler<GetAllProductQuerry, Pagination<ProductResponse>>
    {
        private readonly IProductRepository productRepository;

        public GetAllProductHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<Pagination<ProductResponse>> Handle(GetAllProductQuerry request, CancellationToken cancellationToken)
        {
            var productList = await productRepository.GetProducts(request.CatalogSpecParams);
            var productResponse = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(productList);
            return productResponse;
        }
    }
}

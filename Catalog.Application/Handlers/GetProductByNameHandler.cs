using Catalog.Application.Mappers;
using Catalog.Application.Querries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetProductByNameHandler : IRequestHandler<GetProductByNameQuerry, Pagination<ProductResponse>>
    {
        private readonly IProductRepository productRepository;

        public GetProductByNameHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<Pagination<ProductResponse>> Handle(GetProductByNameQuerry request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetProductByName(request.Name, request.catalogSpecParams);
            var productResponse = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(product);
            return productResponse;
        }
    }
}

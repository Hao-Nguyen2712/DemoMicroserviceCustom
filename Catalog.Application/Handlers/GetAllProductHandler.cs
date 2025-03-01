﻿using Catalog.Application.Mappers;
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
    public class GetAllProductHandler : IRequestHandler<GetAllProductQuerry, IList<ProductResponse>>
    {
        private readonly IProductRepository productRepository;

        public GetAllProductHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<IList<ProductResponse>> Handle(GetAllProductQuerry request, CancellationToken cancellationToken)
        {
            var productList = await productRepository.GetProducts();
            var productResponse = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);
            return productResponse;
        }
    }
}

using AutoMapper;
using Catalog.Application.Mappers;
using Catalog.Application.Querries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuerry, IList<BrandResponse>>
    {
        private readonly IBrandRepository brandRepository;

        public GetAllBrandsHandler(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;

        }
        public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuerry request, CancellationToken cancellationToken)
        {
            var brandList = await brandRepository.GetAllBrands();
            var brandResponse = ProductMapper.Mapper.Map<IList<ProductBrand>, IList<BrandResponse>>(brandList.ToList());
            return brandResponse;
        }
    }
}

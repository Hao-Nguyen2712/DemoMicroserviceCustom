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
    public class GetAllTypeHandler : IRequestHandler<GetAllTypeQuerry, IList<TypeResponse>>
    {
        private readonly ITypeRepository typeRepository;

        public GetAllTypeHandler(ITypeRepository typeRepository)
        {
            this.typeRepository = typeRepository;
        }
        public async Task<IList<TypeResponse>> Handle(GetAllTypeQuerry request, CancellationToken cancellationToken)
        {
            var typeList = await typeRepository.GetAllTypes();
            var typeResponse = ProductMapper.Mapper.Map<IList<TypeResponse>>(typeList);
            return typeResponse;
        }
    }
}

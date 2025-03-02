using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Querries
{
    public class GetProductByNameQuerry : IRequest<Pagination<ProductResponse>>
    {
        public string Name { get; set; }
        public CatalogSpecParams catalogSpecParams { get; set; }
        public GetProductByNameQuerry(string name , CatalogSpecParams catalogSpecParams)
        {
            this.Name = name;
            this.catalogSpecParams = catalogSpecParams;
        }
    }
}

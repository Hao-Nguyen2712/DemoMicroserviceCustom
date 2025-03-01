using Catalog.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Querries
{
    public class GetProductByNameQuerry : IRequest<IList<ProductResponse>>
    {
        public string Name { get; set; }

        public GetProductByNameQuerry(string name)
        {
            this.Name = name;
        }
    }
}

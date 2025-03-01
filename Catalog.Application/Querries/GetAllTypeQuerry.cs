using Catalog.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Querries
{
    public class GetAllTypeQuerry : IRequest<IList<TypeResponse>>
    {
    }
}

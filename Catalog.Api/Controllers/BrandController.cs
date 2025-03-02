using System.Net;
using Asp.Versioning;
using Catalog.Application.Querries;
using Catalog.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiVersion("1.0")]
    public class BrandController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("Brands", Name = "GetBrands")]
        [ProducesResponseType(typeof(BrandResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetBrands(CancellationToken cancellationToken)
        {
            var query = new GetAllBrandsQuerry();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}

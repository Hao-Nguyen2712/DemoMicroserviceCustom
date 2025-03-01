using Asp.Versioning;
using Catalog.Application.Querries;
using Catalog.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Api.Controllers
{
    [ApiVersion("1.0")]
    public class ProductController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> GetProductById(string id, CancellationToken cancellationToken)
        {
            var query = new GetProductByIdQuerry(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> GetProduct(CancellationToken cancellationToken)
        {
            var query = new GetAllProductQuerry();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetProductByName([FromBody] string name, CancellationToken cancellationToken)
        {
            var query = new GetProductByNameQuerry(name);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetProductByBrand([FromBody] string brandName, CancellationToken cancellationToken)
        {
            var query = new GetProductByBrandQuerry(brandName);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);

        }
        [HttpGet]
        public async Task<ActionResult> GetTypes( CancellationToken cancellationToken)
        {
            var query = new GetAllBrandsQuerry();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}

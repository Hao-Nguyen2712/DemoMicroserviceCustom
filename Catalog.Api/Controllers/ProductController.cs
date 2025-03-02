using Asp.Versioning;
using Catalog.Application.Commands;
using Catalog.Application.Querries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
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

        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> GetProductById(string id, CancellationToken cancellationToken)
        {
            var query = new GetProductByIdQuerry(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pagination<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Pagination<ProductResponse>>> GetProducts([FromQuery] CatalogSpecParams catalogSpecParams, CancellationToken cancellationToken)
        {
            var query = new GetAllProductQuerry(catalogSpecParams);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        [Route("ProductName", Name = "GetProductByName")]
        [ProducesResponseType(typeof(Pagination<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetProductByName([FromQuery]string name, CatalogSpecParams specParams , CancellationToken cancellationToken)
        {
          //  var PagingParams = new CatalogSpecParams();
            var query = new GetProductByNameQuerry(name , specParams);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        [Route("brand/{brandName}", Name = "GetProductByBrand")]
        public async Task<ActionResult> GetProductByBrand(string brandName, CancellationToken cancellationToken)
        {
            var query = new GetProductByBrandQuerry(brandName);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateProduct([FromBody] CreateProductCommand createProductCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createProductCommand, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
      
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductCommand updateProductCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(updateProductCommand, cancellationToken);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteProduct(string id, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}

using Asp.Versioning;
using Catalog.Application.Querries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiVersion("1.0")]
    public class TypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("Types", Name = "GetTypes")]
        public async Task<ActionResult> GetTypes(CancellationToken cancellationToken)
        {
            var query = new GetAllTypeQuerry();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}

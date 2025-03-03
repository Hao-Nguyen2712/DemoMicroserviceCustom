using System.Net;
using Asp.Versioning;
using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    // controller for basket
    [ApiVersion(1.0)]
    public class BasketController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userName}", Name = "GetBasketByName")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string userName)
        {
            var query = new GetBasketByUsernameQuerry(userName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
        {
            var result = await _mediator.Send(createShoppingCartCommand);
            return Ok(result);
        }

        [HttpDelete(Name = "DeleteBasket")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteBasket([FromBody]DeleteShoppingCartCommand deleteShoppingCartCommand)
        {
            var result = await _mediator.Send(deleteShoppingCartCommand);
            return Ok(result);
        }
    }
}

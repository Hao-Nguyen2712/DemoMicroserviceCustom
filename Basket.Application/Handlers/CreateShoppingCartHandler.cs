using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Response;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handlers
{
    public class CreateShoppingCartHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _repository;

        public CreateShoppingCartHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            // apply Discount Service
            var shoppingCart = await _repository.UpdateBasket(new ShoppingCart
            {
                UserName = request.UserName,
                Items = request.Items
            });
            var response = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
            return response;
        }
    }
}

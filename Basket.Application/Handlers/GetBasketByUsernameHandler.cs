using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Response;
using Basket.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handlers
{
    public class GetBasketByUsernameHandler : IRequestHandler<GetBasketByUsernameQuerry, ShoppingCartResponse>
    {
        private readonly IBasketRepository _repository;

        public GetBasketByUsernameHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<ShoppingCartResponse> Handle(GetBasketByUsernameQuerry request, CancellationToken cancellationToken)
        {
            var shoppingcart = await _repository.GetBasket(request.UserName);
            var response = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingcart);
            return response;
        }
    }
}

using Basket.Application.Commands;
using Basket.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handlers
{
    public class DeleteShoppingCartHandler : IRequestHandler<DeleteShoppingCartCommand , Unit>
    {
        private readonly IBasketRepository _repository;
        public DeleteShoppingCartHandler(IBasketRepository basketRepository)
        {
            _repository = basketRepository;
        }

        public async Task<Unit> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteBasket(request.UserName);
            return Unit.Value;
        }

    }

}

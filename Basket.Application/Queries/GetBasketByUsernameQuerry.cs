using Basket.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Queries
{
   public class GetBasketByUsernameQuerry :IRequest<ShoppingCartResponse>
    {
        public string UserName { get; set; }

        public GetBasketByUsernameQuerry(string userName)
        {
            UserName = userName;
        }
    }
}

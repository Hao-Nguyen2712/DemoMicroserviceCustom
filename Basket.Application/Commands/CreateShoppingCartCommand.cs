using Basket.Application.Response;
using Basket.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Commands
{
    public class CreateShoppingCartCommand : IRequest<ShoppingCartResponse>
    {
        public string UserName { get; set; }
        public List<ShoppingCartItems> Items { get; set; }

        public CreateShoppingCartCommand(string userName, List<ShoppingCartItems> items)
        {
            UserName = userName;
            Items = items;
        }
    }
}

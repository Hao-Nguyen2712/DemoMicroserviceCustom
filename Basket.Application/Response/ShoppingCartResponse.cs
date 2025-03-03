using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Response
{
    public class ShoppingCartResponse
    {
        public string UserName { get; set; }
        public List<ShoppingCartItemResponse> Items { get; set; }

        public ShoppingCartResponse() { }
        public ShoppingCartResponse(string name)
        {
            UserName = name;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                if (Items != null)
                {
                    foreach (var item in Items)
                    {
                        totalPrice += item.Price * item.Quantity;
                    }
                }
                return totalPrice;
            }
        }
    }
}

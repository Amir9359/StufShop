using System.Collections.Generic;
using StufShop.Models.Stuf;

namespace StufShop.Models.Carts
{
    public class CartItemPayed
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CartPayedId { get; set; }
        public int Quantity { get; set; }
        public CartPayed CartPayed { get; set; }
    }
}
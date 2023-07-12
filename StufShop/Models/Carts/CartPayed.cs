using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StufShop.Models.Carts;

namespace StufShop.Models.Carts
{
    public class CartPayed
    {
        public int Id { get; set; }
        public string Paynumber { get; set; }
        public string Date { get; set; }
        public ICollection<CartItemPayed> CartItemPayed { get; set; }
        public int CartPayedItemID { get; set; }
        public User Cusstomer { get; set; }
    }
}

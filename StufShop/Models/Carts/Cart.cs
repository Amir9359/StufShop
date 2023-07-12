using System;
using System.Collections.Generic;
using StufShop.Models.Carts;
using StufShop.Models;

namespace StufShop.Models.Carts
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public User Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotlaPrice { get; set; }
        public PayState PayState { get; set; }
        public List<CartItems> cartItems { get; set; }

    }
}
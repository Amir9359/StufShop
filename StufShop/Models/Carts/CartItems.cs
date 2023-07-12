using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.Models.Stuf;

namespace StufShop.Models.Carts
{
    public class CartItems
    {
        public long Id { get; set; }
        public Cart Cart { get; set; }
        public Stuf.Stuf Stuf { get; set; }
        public int number { get; set; }

        public double Price { get; set; }
    }
}

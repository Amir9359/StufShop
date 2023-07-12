 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.Models;

namespace StufShop.Models.Stuf
{
    public class StufItem
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
        public byte Quantity { get; set; }

        public Stuf Stuf { get; set; }

        public State state { get; set; }
        public User Creator { get; set; }
        public DateTime CreateDate { get; set; }

    }
}

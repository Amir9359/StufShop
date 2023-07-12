using StufShop.Models.Stuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.Models.Groups;

namespace StufShop.Models.ViewModels.Product
{
    public class StufShopView
    {
        public int Id { get; set; }
        public string PrimaryTitle { get; set; }
        public string SecondaryTitle { get; set; }
        public string Description { get; set; }
        public Group group { get; set; }
        public int Count { get; set; }
 
        public State state { get; set; }

        public string Creator { get; set; }
        public string CreateDate { get; set; }

    }
}

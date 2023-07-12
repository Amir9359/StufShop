using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.Models;
using StufShop.Models.Stuf;

namespace StufShop.Models.ViewModels.Product
{
    public class GoupBrandView
    {
        public SelectList Brands { get; set; }
        public SelectList Groups { get; set; }
        public Stuf.Stuf StufShop { get; set; }
    }
}

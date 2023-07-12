using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.Models.Stuf;
using StufShop.Migrations;

namespace StufShop.Models.Stuf
{
    public class GoupBrandView
    {
        public SelectList Brands { get; set; }
        public SelectList Groups { get; set; }
        public Stuf StufShop { get; set; }
    }
}

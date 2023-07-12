using System;
using System.Collections.Generic;
using StufShop.Models.Groups;
using StufShop.Models;
using StufShop.Models.Stuf;

namespace StufShop.Models.Stuf
{
    public class Stuf
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Group group { get; set; }
        public int groupid { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public State state { get; set; }

        public User Creator { get; set; }
        public DateTime CreateDate { get; set; }

        public List<StufItem> StufItems { get; set; }
    }
}
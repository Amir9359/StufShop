using System;
using StufShop.Models;

namespace StufShop.Models.Groups
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupTitle { get; set; }
        public string GroupSlug { get; set; }
        public User Creator { get; set; }

    }
}
using StufShop.Models;

namespace StufShop.Models.Profile
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string city { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }

        public User Customer { get; set; }
    }
}
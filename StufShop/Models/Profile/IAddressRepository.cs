using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StufShop.Models.Profile
{
   public  interface IAddressRepository
    {
        Task Add(Address address);
        Task<Address> Find(string CustomerId);
        Task Save();
    }
}

using Microsoft.EntityFrameworkCore;
using StufShop.Models;
using StufShop.Models.Profile;
using System.Threading.Tasks;

namespace StufShop.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private ApplicationDbContext _context;
        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Address address)
        {
            await _context.Addresses.AddAsync(address);
        }

        public async Task<Address> Find(string customerId)
        {
            var Address = await _context.Addresses.SingleOrDefaultAsync(c => c.Customer.Id == customerId);
            return Address;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using StufShop.InfraStructure;
using StufShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.Models.Carts;


namespace StufShop.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private ApplicationDbContext _Context;
        public CartItemRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task Add(Cart order)
        {
            await _Context.Carts.AddAsync(order);
        }
        public async Task<CartItems> FindItem(int id)
        {
            var cartItem = await _Context.cartItems.Include(c => c.Stuf)
                .SingleOrDefaultAsync(c => c.Id == id);
            return cartItem;
        }
        public async Task<bool> DeleteCartItem(int id)
        {
            var cartItem = await _Context.cartItems.SingleOrDefaultAsync(s => s.Id == id);
            if (cartItem != null)
            {
                _Context.cartItems.Remove(cartItem);
                return true;
            }

            return false;
        }
        public async Task Add(List<CartItems> orderItems)
        {
            await _Context.cartItems.AddRangeAsync(orderItems);
        }
        public async Task<List<CartItems>> search(string customerId)
        {
            var payItems = await _Context.cartItems.Include(d => d.Stuf).Where(s => s.Cart.Customer.Id == customerId).ToListAsync();

            return payItems;
        }
        public async Task<Cart> Find(int orderId)
        {
            var order = await _Context.Carts
               .Include(o => o.cartItems)
               .ThenInclude(p=>p.Stuf)
               .ThenInclude(p => p.StufItems)
               .Include(c => c.Customer)
               .FirstOrDefaultAsync(o => o.Id == orderId);
            return order;
        }

        public async Task Save()
        {
            await _Context.SaveChangesAsync();
        }

        public async Task  Update(int orderid, string FishNumber, string PayDate)
        {
            var ord =await _Context.Carts.FindAsync(orderid);
            ord.PayState = PayState.Paied;

            _Context.Entry(ord).Reference(c => c.Customer).IsModified = false;

        }
    }
}

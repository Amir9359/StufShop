using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.Models;
using StufShop.Models.Carts;

namespace StufShop.Repository
{
    public class CartRopository : ICartRopository
    {
        private ApplicationDbContext _Context;
        public CartRopository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task Add(Cart cart)
        {
            await _Context.Carts.AddAsync(cart);
        }

        public async Task Add(CartItems cartItem)
        {
            await _Context.cartItems.AddAsync(cartItem);
        }


        public void DeletCartItems(List<CartItems> CartItems)
        {
            _Context.cartItems.RemoveRange(CartItems);
        }

 

        public async Task Delete(int cartItemId)
        {
            var cartItem = await _Context.cartItems.FindAsync(cartItemId);
            _Context.cartItems.Remove(cartItem);
        }

 
        public async Task DeleteCart(int cartId)
        {
            var Cart=  await _Context.Carts.FindAsync(cartId);
            _Context.Carts.Remove(Cart);
        }

 
        public async Task<Cart> Find(int cartid)
        {
            var cart = await _Context.Carts.FindAsync(cartid);
            return cart;
        }

        public async Task<Cart> Find(string customerid)
        {
            var cart = await _Context.Carts.Include(c => c.Customer)
                .Include(c => c.cartItems)
                .ThenInclude(i => i.Stuf)
                .SingleOrDefaultAsync(c => c.Customer.Id == customerid);
            return cart;
        }

        public async Task save()
        {
            await _Context.SaveChangesAsync();
        }

        public async Task<List<Cart>> search(string customeerId)
        {
            var carts = await _Context.Carts.Where(c => c.Customer.Id == customeerId)
               .Include(c => c.Customer).ToAsyncEnumerable().ToList();
            return carts;
        }

        public async Task Update(CartItems item)
        {
            var cartItem = await _Context.cartItems.FindAsync(item.Id);
            var Quantity = item.number + cartItem.number;
            cartItem.Id = item.Id;
            cartItem.number = Quantity;

            _Context.cartItems.Update(cartItem);
            _Context.Entry(cartItem).Reference(c => c.Cart).IsModified = false;
            _Context.Entry(cartItem).Reference(c => c.Stuf).IsModified = false;

        }
 
    }
}

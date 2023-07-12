using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.Models.Carts;

namespace StufShop.Models.Carts
{
    public interface ICartRopository
    {
        Task Add(Cart cart);
        Task Add(CartItems cartItem);
        Task<Cart> Find(int cartid);
        Task<Cart> Find(string customerid);
        Task<List<Cart>> search(string customerId);
        Task Update(CartItems item);
        Task Delete(int cartItemId);
 
        Task DeleteCart(int cartId);
        void  DeletCartItems(List<CartItems> CartItems);
 
        Task save();
    }
}

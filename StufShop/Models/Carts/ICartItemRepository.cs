using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.Models.Carts;
 

namespace StufShop.Models.Carts
{
   public interface ICartItemRepository
    {
        Task Add(Cart order);
        Task Add(List<CartItems> orderItems);
        Task<Cart> Find(int orderId);
        Task<CartItems> FindItem(int id);
        Task<bool> DeleteCartItem(int id);
        Task Update(int orderid, string FishNumber , string PayDate);
        Task Save();
        Task<List<CartItems>> search(string customerId);

    }
}

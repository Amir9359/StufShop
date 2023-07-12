using Microsoft.EntityFrameworkCore;
using StufShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using StufShop.Models.Stuf;
using StufShop.Models.Carts;

namespace StufShop.Repository
{
    public class StufItemRepository : IStufItemRepository
    {
    private readonly  ApplicationDbContext context;
        public StufItemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task Add(StufItem StufItem)
        {
            await context.StufItems.AddAsync(StufItem);
        }
 
        public async Task Delete(int? id)
        {
            var CartItems = await context.cartItems.FindAsync(id);
            context.Remove(CartItems);
        }

        public async Task<StufItem> Find(int id)
        {
            var StufItems = await context.StufItems
                .Include(t=>t.Stuf)
                .Where(p=>p.Id == id).FirstOrDefaultAsync();
            return StufItems;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task<List<StufItem>> search(int stufid)
        {
            var query = await context.StufItems.Include(t => t.Stuf)
                .Include(t => t.Creator).ToAsyncEnumerable().ToList();
            var stufitem = query.Where(t => t.Stuf.Id == stufid).ToList();
            return stufitem;
        }

        public async Task Update(StufItem FoodItem)
        {
            var pitem = await context.StufItems.Include(t => t.Stuf).Where(p => p.Id == FoodItem.Id).FirstOrDefaultAsync();
            pitem.Id = FoodItem.Id;
            pitem.Price = FoodItem.Price;
            pitem.state = FoodItem.state;
            pitem.Quantity = FoodItem.Quantity;
            pitem.Discount = FoodItem.Discount;

            context.StufItems.Update(pitem);
            context.Entry(FoodItem).Property(p => p.CreateDate).IsModified = false;
            context.Entry(FoodItem).Reference(p => p.Creator).IsModified = false;
            context.Entry(FoodItem).Reference(p => p.Stuf).IsModified = false;
        }

        public async Task Update(List<StufItem> FoodItem)
        {
            var itemlist = new List<StufItem>();
             
            foreach (var item in FoodItem)
            {
                var pitem =await context.StufItems.FindAsync(item.Id);
                pitem.Quantity = (byte)(pitem.Quantity - item.Quantity);

                context.Entry(pitem).Property(p => p.CreateDate).IsModified = false;
                context.Entry(pitem).Reference(p => p.Creator).IsModified = false;
                context.Entry(pitem).Reference(p => p.Stuf).IsModified = false;
                itemlist.Add(pitem);
            }
             context.UpdateRange(itemlist);
        }
 
    }
}

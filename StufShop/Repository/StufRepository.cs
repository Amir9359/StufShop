using Microsoft.EntityFrameworkCore;
using StufShop.Models;
using StufShop.Models.Stuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.InfraStructure;

namespace StufShop.Repository
{
    public class StufRepository : IStufRepository
    {
        private ApplicationDbContext context;
        public StufRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Stuf Food)
        {
            await context.AddAsync(Food);
        }

        public async Task DeleteAsync(int id)
        {
            Stuf p = await context.Stufs.FindAsync(id);
            context.Remove(p);
        }

        public async Task<Stuf> FindAsync(int id)
        {
            return await context.Stufs.Include(b => b.group)
                .Where(p => p.Id == id).ToAsyncEnumerable().SingleOrDefault();
        }


        public async Task<Stuf> StufDetailAsync(int Stufid)
        {
            return await context.Stufs
                    .Include(g => g.group)
                .Include(item => item.StufItems)
                .Where(p => p.Id == Stufid)
                .FirstOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Stuf>> SearchAsync(int? id, string PrimaryTitle)
        {
            var query = await context.Stufs
                .Include(b => b.Creator).Include(g => g.group)
                .Where(p => (p.Id == id || id == null) && (p.Title == PrimaryTitle || PrimaryTitle.CheckStringIsnull()))
                .ToAsyncEnumerable().ToList();
            return query;
        }


        public async Task<IEnumerable<Stuf>> SearchAsync(int Groupid)
        {
            var query = await context.Stufs
                .Include(g => g.group).Include(item => item.StufItems)
            .Where(p => (p.groupid == Groupid)).ToAsyncEnumerable().ToList();
            return query;
        }

        public void Update(Stuf Food)
        {//اگر تنها اپدیت کنیم مقدار زمان ایجاد 000 0000 میشود  پس باید مقادیری که میخواهیم اپدیت شود تغییر دهیم
            context.Stufs.Update(Food);
            context.Entry(Food).Property(p => p.CreateDate).IsModified = false;
            context.Entry(Food).Reference(p => p.Creator).IsModified = false;
        }
    }
}

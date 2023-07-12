using Microsoft.EntityFrameworkCore;
using StufShop.Models;
using StufShop.Models.Stuf;
using StufShop.Models.Groups;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StufShop.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private ApplicationDbContext context;
        public GroupRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Group group)
        {
           await context.Groups.AddAsync(group);
        }

        public async Task DeleteAsync(int id)
        {
            Group g =  await context.Groups.FindAsync(id);
            context.Groups.Remove(g);
        }

        public async Task<Group> FindAsync(int id) 
        {
            var f= await context.Groups.Include(o=>o.Creator).Where(g=>g.Id==id).ToAsyncEnumerable().SingleOrDefault();
            return f;
        }
        public async Task<List<Group>> FindGroupsAsync( ) 
        {
            var f= await context.Groups.Include(o=>o.Creator)
                    .ToAsyncEnumerable().ToList();
            return f;
        }

        public async Task SaveAsync()
        {
          await  context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Group>> SearchAsync(string title, int? id, State? state)
        {
            var Query = await context.Groups.Include(o => o.Creator)
                .ToAsyncEnumerable().ToList();
            var Groups = await Query.Where(b => (b.GroupTitle == title || string.IsNullOrEmpty(title)) 
                                                && (b.Id == id || id == null)).ToAsyncEnumerable().ToList();

            return Groups;
        }
        public async Task Update(Group group)
        {
            var grp= await context.Groups.FindAsync(group.Id);
            grp.Id = group.Id;
            grp.GroupTitle = group.GroupTitle;
            grp.GroupSlug = group.GroupSlug;

        }
    }
}

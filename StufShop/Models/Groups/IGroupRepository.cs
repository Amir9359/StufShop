using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.Models.Groups;
using StufShop.Models.Stuf;

namespace StufShop.Models.Groups
{
    public interface IGroupRepository
    {
        Task AddAsync(Group group);
        Task Update(Group group);
        Task DeleteAsync(int id);
        Task<Group> FindAsync(int id);
        Task<List<Group>> FindGroupsAsync();
        Task<IEnumerable<Group>> SearchAsync(string title, int? id, State? state);
        Task SaveAsync();
    }
}

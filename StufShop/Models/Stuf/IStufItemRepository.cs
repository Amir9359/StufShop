using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StufShop.Models.Stuf
{
    public interface IStufItemRepository
    {
        Task Add(StufItem StufItem);
        Task Update(StufItem StufItem);
        Task Update(List<StufItem> StufItem);
        Task Delete(int? id);
        Task<List<StufItem>> search(int StufItem);
        Task<StufItem> Find(int id);
        Task Save();
    }
}

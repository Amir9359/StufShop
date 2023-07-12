using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StufShop.Models.Stuf;

namespace StufShop.Models.Stuf
{
   public  interface IStufRepository
    {
        Task AddAsync(Stuf StufShop);
        void Update(Stuf StufShop);
        Task DeleteAsync(int Id);
        Task<Stuf> FindAsync(int id);
        Task<Stuf> StufDetailAsync(int Stufid);
        Task<IEnumerable<Stuf>> SearchAsync(int? id , string PrimaryTitle);
        Task<IEnumerable<Stuf>> SearchAsync(int Groupid);
        Task SaveAsync();
    }
}
